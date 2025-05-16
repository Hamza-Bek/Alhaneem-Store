using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Product;

public class SellProductRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
