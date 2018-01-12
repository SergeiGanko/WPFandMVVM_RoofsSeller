using RoofsSeller.Model.Entities;

namespace RoofsSeller.UI.Wrapper
{
    public class ProductDiscountWrapper : ModelWrapper<ProductDiscount>
    {
        public ProductDiscountWrapper(ProductDiscount model) : base(model)
        {
        }

        public int Id { get { return Model.Id; } }

        public double Rate
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }
    }
}
