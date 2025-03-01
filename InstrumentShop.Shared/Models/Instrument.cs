using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstrumentShop.Shared.Models
{
    public class Instrument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InstrumentId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [MaxLength(200)]
        public string ImageUrl { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public InstrumentCategory Category { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
    }
}