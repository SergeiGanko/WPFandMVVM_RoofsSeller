using RoofsSeller.Model.Entities;

namespace RoofsSeller.UI.Wrapper
{
    public class ProductMeasureWrapper : ModelWrapper<ProductMeasure>
    {
        public ProductMeasureWrapper(ProductMeasure model) : base(model)
        {
        }

        public int Id { get { return Model.Id; } }

        public string Measure
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
    }
}
