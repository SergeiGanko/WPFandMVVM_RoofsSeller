using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoofsSeller.Model.Entities
{
    public class OrderItem
    {
        [Key, ForeignKey("Order")]
        [Column(Order = 0)]
        public int OrderId { get; set; }

        [Key, ForeignKey("Product")]
        [Column(Order = 1)]
        public int ProductId { get; set; }

        [Required, Range(0, 1000)]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public decimal TotalPrice { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Required]
        public Order Order { get; set; }

        [Required]
        public Product Product { get; set; }
    }
}
