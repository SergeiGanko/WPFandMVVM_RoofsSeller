using System.Windows.Media.Imaging;

namespace RoofsSeller.Model.Entities
{
    public class Slope
    {
        public string SlopeType { get; set; }
        public BitmapImage SlopeImage { get; set; }
        public int SideA { get; set; }
        public int SideB { get; set; }
        public int SlopeHeight { get; set; }
        public double SlopeSquare { get; set; }
        public decimal ModuleCost { get; set; }
        public double ModuleEffectiveSquare { get; set; }
        public int ModuleQuantity { get; set; }
        public decimal Summ { get; set; }
    }
}
