
namespace Application.Dtos.Admin;
public class SalesStatisticsDto
{
    public DateTime Date { get; set; }
    public int TotalOrders { get; set; }
    public decimal TotalRevenue { get; set; }
    public int TotalProductsSold { get; set; }
    public decimal TotalSales { get; set; }
    public decimal ShippingFees { get; set; }
}
