using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models;

public class SalesStatistics : EntityBase
{
    public DateTime Date { get; set; }
    public int TotalOrders { get; set; } // Total number of orders
    public decimal TotalRevenue { get; set; } // Total revenue from sales
    public int TotalProductsSold { get; set; } // Total number of products sold
    public decimal TotalSales { get; set; } // Total number of sales transactions
    public decimal ShippingFees { get; set; } // Total of shipping collected from sales    
}