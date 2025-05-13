
using Application.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly AppDbContext _context;

    public AdminRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<SalesStatistics> GetSalesStatisticsAsync(DateTime date)
    {
        SalesStatistics data;

        if (date != default)
        {
            data = await _context.SalesStatistics
                .Where(s => s.Date == date)
                .FirstOrDefaultAsync();
        }
        else
        {
            data = new SalesStatistics
            {
                TotalOrders = await _context.SalesStatistics.SumAsync(s => s.TotalOrders),
                TotalRevenue = await _context.SalesStatistics.SumAsync(s => s.TotalRevenue),
                TotalProductsSold = await _context.SalesStatistics.SumAsync(s => s.TotalProductsSold),
                TotalSales = await _context.SalesStatistics.SumAsync(s => s.TotalSales),
                ShippingFees = await _context.SalesStatistics.SumAsync(s => s.ShippingFees),
                Date = DateTime.UtcNow 
            };
        }

        return data;
    }
}
