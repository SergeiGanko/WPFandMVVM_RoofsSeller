using RoofsSeller.Model.Entities;

namespace RoofsSeller.UI.Wrapper
{
    public class RoofWrapper : ModelWrapper<Roof>
    {
        public RoofWrapper(Roof model)
            : base(model)
        {
        }

        public double RoofSquare
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }

        public int TotalQuantity
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public decimal TotalSum
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }
    }
}
