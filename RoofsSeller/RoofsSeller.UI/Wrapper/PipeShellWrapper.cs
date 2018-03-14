using RoofsSeller.Model.Entities;

namespace RoofsSeller.UI.Wrapper
{
    public class PipeShellWrapper : ModelWrapper<PipeShell>
    {
        public PipeShellWrapper(PipeShell model) : base(model)
        {
        }

        public int MainBarHeightSide13
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int MainBarHeightSide24
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int MainBarWidthSide13
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int MainBarWidthSide24
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int MainBarQuantitySide13
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int MainBarQuantitySide24
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public double MainBarMetalRequired
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }

        public int InnerBarHeightSide13
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int InnerBarHeightSide24
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int InnerBarWidthSide13
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int InnerBarWidthSide24
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int InnerBarQuantitySide13
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int InnerBarQuantitySide24
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int CornerBarHeight
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int CornerBarWidth
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int SubBarHeightSide13
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int SubBarWidthSide13
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int SubBarHeightSide24
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int SubBarWidthSide24
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public double InnerCornerSubBarMetalRequired
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }

        public double MetalRequired
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }

        public string KindOfShell
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public decimal ShellCost
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }
    }
}
