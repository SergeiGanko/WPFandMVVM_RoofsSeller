using RoofsSeller.Model.Entities;

namespace RoofsSeller.UI.Wrapper
{
    public class ProductTypeWrapper : ModelWrapper<ProductType>
    {
        public ProductTypeWrapper(ProductType model) : base(model)
        {
        }

        public int Id { get { return Model.Id; } }

        public string Type
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
    }
}
