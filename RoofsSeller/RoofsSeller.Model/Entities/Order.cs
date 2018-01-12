using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoofsSeller.Model.Entities
{
    public class Order
    {
        public Order()
        {
            OrderItems = new Collection<OrderItem>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime OrderDate { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime? ShippingDate { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public decimal TotalAmount { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public int? CustomerId { get; set; }

        public Customer Customer { get; set; }

        public int StateId { get; set; } = 1;

        public OrderState State { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
