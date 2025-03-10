    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace InstrumentShop.Shared.Models
    {
    public class OrderDetail
    {
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; } // Thêm thuộc tính Order

        [ForeignKey("Instrument")]
        public int InstrumentId { get; set; }
        public Instrument Instrument { get; set; } // Thêm thuộc tính Instrument

        public int Quantity { get; set; }

        public decimal Price { get; set; }
        
    }
}