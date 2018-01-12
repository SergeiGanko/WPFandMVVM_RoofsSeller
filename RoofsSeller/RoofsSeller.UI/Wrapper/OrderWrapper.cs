using RoofsSeller.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoofsSeller.UI.Wrapper
{
    public class OrderWrapper : ModelWrapper<Order>
    {
        public OrderWrapper(Order model) : base(model)
        {
        }

        public int Id { get { return Model.Id; } }

        public int OrderNumber
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public DateTime OrderDate
        {
            get { return GetValue<DateTime>(); }
            set
            {
                SetValue(value);
                if (ShippingDate < OrderDate)
                {
                    OrderDate = ShippingDate;
                }
            }
        }

        public DateTime ShippingDate
        {
            get { return GetValue<DateTime>(); }
            set
            {
                SetValue(value);
                if (ShippingDate < OrderDate)
                {
                    ShippingDate = OrderDate;
                }
            }
        }

        public int? StateId
        {
            get { return GetValue<int?>(); }
            set { SetValue(value); }
        }

        public int? CustomerId
        {
            get { return GetValue<int?>(); }
            set { SetValue(value); }
        }

        public decimal TotalAmount
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }
    }
}
