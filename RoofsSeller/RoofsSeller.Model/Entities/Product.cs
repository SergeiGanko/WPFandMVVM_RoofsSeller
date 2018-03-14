using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoofsSeller.Model.Entities
{
    public class Product
    {
        public Product()
        {
            OrderItems = new Collection<OrderItem>();
        }

        [Key]
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public decimal Price { get; set; }

        [Required, Range(0, 1000000)]
        public int StockBalance { get; set; }

        [StringLength(500)]
        public string Info { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

        public int? ProductDiscountId { get; set; }

        public ProductDiscount ProductDiscount { get; set; }

        public int ProductTypeId { get; set; } = 9;

        public ProductType ProductType { get; set; }

        public int ProductMeasureId { get; set; }

        public ProductMeasure ProductMeasure { get; set; }

        public int? ProviderId { get; set; }

        public Provider Provider { get; set; }
    }
}
