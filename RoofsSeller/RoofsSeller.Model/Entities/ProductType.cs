using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace RoofsSeller.Model.Entities
{
    public class ProductType
    {
        public ProductType()
        {
            Products = new Collection<Product>();
        }

        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Type { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
