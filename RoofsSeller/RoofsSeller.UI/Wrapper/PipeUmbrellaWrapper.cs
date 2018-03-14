using RoofsSeller.Model.Entities;

namespace RoofsSeller.UI.Wrapper
{
    public class PipeUmbrellaWrapper : ModelWrapper<PipeUmbrella>
    {
        public PipeUmbrellaWrapper(PipeUmbrella model) : base(model)
        {
        }

        public string KindOfUmbrella
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public int FitWidthSide13
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int FitLengthSide13
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int FitQuantitySide13
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int FitWidthSide24
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int FitLengthSide24
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int FitQuantitySide24
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int LegWidth
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int LegHeight
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int LegQuantity
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int SubBarWidth
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int SubBarHeight
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int SubBarQuantity
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int BottomPartWidth
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int BottomPartLength
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int ValmaWidth
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int ValmaLength
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int TriangleWidth
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int TriangleLength
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int LouversLengthSide13
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int LouversWidthSide13
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int LouversLengthSide24
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int LouversWidthSide24
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public double MetalRequired
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }

        public decimal UmbrellaCost
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }
    }
}
