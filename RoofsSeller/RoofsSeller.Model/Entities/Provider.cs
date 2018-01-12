using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace RoofsSeller.Model.Entities
{
    public class Provider
    {
        public Provider()
        {
            Products = new Collection<Product>();
        }

        public int Id { get; set; }

        [Required, StringLength(60)]
        public string Name { get; set; }

        [Required, StringLength(100)]
        public string Address { get; set; }

        [Required, Phone()]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(100)]
        public string Info { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
