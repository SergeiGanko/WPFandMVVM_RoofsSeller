using RoofsSeller.Model.Entities;

namespace RoofsSeller.UI.Wrapper
{
    public class ProductWrapper : ModelWrapper<Product>
    {
        public ProductWrapper(Product model) : base(model)
        {
        }

        public int Id { get { return Model.Id; } }

        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public decimal Price
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }

        public int StockBalance
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public string Info
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public int? ProductDiscountId
        {
            get { return GetValue<int?>(); }
            set { SetValue(value); }
        }

        public ProductDiscount ProductDiscount
        {
            get { return GetValue<ProductDiscount>(); }
            set { SetValue(value); }
        }

        public ProductType ProductType
        {
            get { return GetValue<ProductType>(); }
            set { SetValue(value); }
        }

        public int? ProductTypeId
        {
            get { return GetValue<int?>(); }
            set { SetValue(value); }
        }

        public int? ProductMeasureId
        {
            get { return GetValue<int?>(); }
            set { SetValue(value); }
        }

        public ProductMeasure ProductMeasure
        {
            get { return GetValue<ProductMeasure>(); }
            set { SetValue(value); }
        }

        public int? ProviderId
        {
            get { return GetValue<int?>(); }
            set { SetValue(value); }
        }
    }
}
