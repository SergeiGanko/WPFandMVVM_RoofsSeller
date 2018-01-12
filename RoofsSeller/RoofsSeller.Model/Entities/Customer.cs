using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace RoofsSeller.Model.Entities
{
    public class Customer
    {
        public Customer()
        {
            Orders = new Collection<Order>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(100)]
        public string Info { get; set; }

        [Required, Phone]
        public string Phone { get; set; }

        [EmailAddress, StringLength(50)]
        public string Email { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
