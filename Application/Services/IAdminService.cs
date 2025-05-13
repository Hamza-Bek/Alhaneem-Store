using Application.Dtos.Admin;

namespace Application.Services;

public interface IAdminService
{
    Task<SalesStatisticsDto> GetSalesStatisticsAsync(DateTime? date = null);   
}
