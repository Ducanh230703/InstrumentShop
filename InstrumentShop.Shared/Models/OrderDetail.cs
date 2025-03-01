using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstrumentShop.Shared.Models
{
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetailId { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int OrderDetailInstrumentId { get; set; } // Đổi tên
        public Instrument Instrument { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}