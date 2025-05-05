using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Cart
{
    public class UpdateCartItemRequest
    {
        public Guid ProductId { get; set; }
        public int QuantityDelta { get; set; } // +1 or -1
        public string SessionId { get; set; } = string.Empty;
    }
}
