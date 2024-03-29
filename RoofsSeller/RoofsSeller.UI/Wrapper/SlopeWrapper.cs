﻿using System.Windows.Media.Imaging;
using RoofsSeller.Model.Entities;

namespace RoofsSeller.UI.Wrapper
{
    public class SlopeWrapper : ModelWrapper<Slope>
    {
        public SlopeWrapper(Slope model) : base(model)
        {
        }

        public string SlopeType
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public BitmapImage SlopeImage
        {
            get { return GetValue<BitmapImage>(); }
            set { SetValue(value); }
        }

        public double SideA
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }

        public double SideB
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }

        public double SlopeHeight
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }

        public double SlopeSquare
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }

        public decimal ModuleCost
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }

        public double ModuleEffectiveSquare
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }

        public int ModuleQuantity
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public decimal Summ
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }
    }
}
