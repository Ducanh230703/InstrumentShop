using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstrumentShop.Shared.Models
{
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeedbackId { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int FeedbackInstrumentId { get; set; } // Đổi tên
        public Instrument Instrument { get; set; }

        [Required]
        [MaxLength(500)]
        public string Comment { get; set; }

        public DateTime FeedbackDate { get; set; } = DateTime.UtcNow;

        [Range(1, 5)]
        public int Rating { get; set; }
    }
}