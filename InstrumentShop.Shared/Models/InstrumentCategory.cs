using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstrumentShop.Shared.Models
{
    public class InstrumentCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InstrumentCategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; }
        public InstrumentCategory()
        {
            Instruments = new List<Instrument>();
        }

        public ICollection<Instrument> Instruments { get; set; }
    }
}