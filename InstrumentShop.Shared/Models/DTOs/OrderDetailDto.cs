using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentShop.Shared.Models.DTOs
{
        public class OrderDetailDto
        {
        public int OrderId { get; set; }
        public int InstrumentId { get; set; }
        public string InstrumentName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
