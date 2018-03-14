using RoofsSeller.Model.Entities;

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
