using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstrumentShop.Shared.Models
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [MaxLength(50)]
        public string PaymentMethod { get; set; } // Ví dụ: Credit Card, PayPal, Momo

        public decimal Amount { get; set; }

        [MaxLength(50)]
        public string PaymentStatus { get; set; } // Ví dụ: Thành công, Thất bại, Đang chờ
    }
}