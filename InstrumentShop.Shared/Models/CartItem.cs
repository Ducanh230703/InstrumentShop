using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstrumentShop.Shared.Models
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartItemId { get; set; }

        public int CartId { get; set; }
        public Cart Cart { get; set; }

        public int CartItemInstrumentId { get; set; } // Đổi tên
        public Instrument Instrument { get; set; }

        public int Quantity { get; set; }
    }
}