using Domain.Models;

namespace Application.Interfaces;

public interface IAdminRepository
{
    Task<SalesStatistics> GetSalesStatisticsAsync(DateTime date);

}
