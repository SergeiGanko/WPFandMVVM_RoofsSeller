using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace RoofsSeller.Model.Entities
{
    public class OrderState
    {
        public OrderState()
        {
            Orders = new Collection<Order>();
        }

        [Required]
        public int Id { get; set; }

        [Required, StringLength(20)]
        public string State { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public ICollection <Order> Orders { get; set; }
    }
}
