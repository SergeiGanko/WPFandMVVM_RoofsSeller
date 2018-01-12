using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace RoofsSeller.Model.Entities
{
    public class ProductDiscount
    {
        public ProductDiscount()
        {
            Products = new Collection<Product>();
        }

        [Key]
        public int Id { get; set; }

        [Required, Range(0, 100)]
        public int Rate { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
