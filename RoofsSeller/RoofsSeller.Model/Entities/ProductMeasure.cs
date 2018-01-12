using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace RoofsSeller.Model.Entities
{
    public class ProductMeasure
    {
        public ProductMeasure()
        {
            Products = new Collection<Product>();
        }

        [Key]
        public int Id { get; set; }

        [Required, StringLength(20)]
        public string Measure { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
