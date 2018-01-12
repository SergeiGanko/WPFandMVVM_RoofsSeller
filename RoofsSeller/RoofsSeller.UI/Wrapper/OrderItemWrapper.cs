using RoofsSeller.Model.Entities;

namespace RoofsSeller.UI.Wrapper
{
    public class OrderItemWrapper : ModelWrapper<OrderItem>
    {
        public OrderItemWrapper(OrderItem model) : base(model)
        {
        }

        public int OrderId { get { return Model.OrderId; } }

        public int ProductId { get { return Model.ProductId; } }

        public int Quantity
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public decimal UnitPrice
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }

        public decimal TotalPrice
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }

        public Order Order
        {
            get { return GetValue<Order>(); }
            set { SetValue(value); }
        }

        public Product Product
        {
            get { return GetValue<Product>(); }
            set { SetValue(value); }
        }
    }
}
