using RoofsSeller.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoofsSeller.UI.Wrapper
{
    public class OrderStateWrapper : ModelWrapper<OrderState>
    {
        public OrderStateWrapper(OrderState model) : base(model)
        {
        }

        public int Id { get { return Model.Id; } }

        public string State
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
    }
}
