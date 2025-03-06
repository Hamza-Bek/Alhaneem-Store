using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Application.Options;
using Application.Validators;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Options;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using FluentValidation;
using FluentValidation.AspNetCore;
using LockoutOptions = Infrastructure.Options.LockoutOptions;
using PasswordOptions = Infrastructure.Options.PasswordOptions;

namespace WebAPI.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var passwordOptions = new PasswordOptions();
        configuration.GetSection("Password").Bind(passwordOptions);

        var lockoutOptions = new LockoutOptions();
        configuration.GetSection("Lockout").Bind(lockoutOptions);

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection") ??
                                 throw new InvalidOperationException("Connection string is not found!"),
                sqlOptions => sqlOptions.MigrationsAssembly("WebAPI")); // 👈 Specify migrations assembly
        });
        
        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Lockout.AllowedForNewUsers = lockoutOptions.AllowedForNewUsers;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(lockoutOptions.DefaultLockoutTimeSpan);
                options.Lockout.MaxFailedAccessAttempts = lockoutOptions.MaxFailedAccessAttempts;

                options.Password.RequireDigit = passwordOptions.RequireDigit;
                options.Password.RequireLowercase = passwordOptions.RequireLowercase;
                options.Password.RequireNonAlphanumeric = passwordOptions.RequireNonAlphanumeric;
                options.Password.RequireUppercase = passwordOptions.RequireUppercase;
                options.Password.RequiredLength = passwordOptions.RequiredLength;
                options.Password.RequiredUniqueChars = passwordOptions.RequiredUniqueChars;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        
        var jwtSettings = new JwtSettings();
        configuration.GetSection("Jwt").Bind(jwtSettings);
        services.AddScoped(_ => jwtSettings);

        var refreshTokenSettings = new RefreshTokenSettings();
        configuration.GetSection("RefreshToken").Bind(refreshTokenSettings);
        services.AddScoped(_ => refreshTokenSettings);

        var key = Encoding.UTF8.GetBytes(jwtSettings.Secret ?? throw new InvalidOperationException("JWT Secret is missing"));
        
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                };
            });
        
        return services;
    }
    
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        
        services.AddHttpContextAccessor();

        
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
        
        services.AddScoped<UserIdentity>(sp =>
        {
            var userIdentity = new UserIdentity();
            var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
            var httpContext = httpContextAccessor.HttpContext;
        
            if (httpContext?.User?.Identity?.IsAuthenticated == true)
            {
                var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (Guid.TryParse(userId, out var id))
                    userIdentity.Id = id;
        
            }
            return userIdentity;
        });
        
        return services;
    }
}