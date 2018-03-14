using RoofsSeller.Model.Entities;

namespace RoofsSeller.UI.Wrapper
{
    public class PipeAbutmentWrapper : ModelWrapper<PipeAbutment>
    {
        public PipeAbutmentWrapper(PipeAbutment model) : base(model)
        {
        }

        public int SideWidth
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int SideLength
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int FrontWidth
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int FrontLength
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int BackWidth
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int BackLength
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int MetalSheetQuantityRequired
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public decimal AbutmentCost
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }

    }
}
