using Application.Dtos.Admin;
using Domain.Models;

namespace Application.Mappers;

public static class SalesStatisticsMapper
{
    public static SalesStatisticsDto ToDto(this SalesStatistics salesStatistics)
    {
        return new SalesStatisticsDto
        {
            Date = salesStatistics.Date,
            TotalOrders = salesStatistics.TotalOrders,
            TotalRevenue = salesStatistics.TotalRevenue,
            TotalSales = salesStatistics.TotalSales,
            TotalProductsSold = salesStatistics.TotalProductsSold,
            ShippingFees = salesStatistics.ShippingFees,
        };
    }
}