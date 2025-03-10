using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentShop.Shared.Models.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public CustomerDto Customer { get; set; }
        public ICollection<OrderDetailDto> OrderDetails { get; set; }
    }
}
