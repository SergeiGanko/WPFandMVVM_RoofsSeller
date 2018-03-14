namespace RoofsSeller.Model.Entities
{
    public class PipeShell
    {
        public int MainBarHeightSide13 { get; set; }
        public int MainBarHeightSide24 { get; set; }
        public int MainBarWidthSide13 { get; set; }
        public int MainBarWidthSide24 { get; set; }
        public int MainBarQuantitySide13 { get; set; }
        public int MainBarQuantitySide24 { get; set; }
        public double MainBarMetalRequired { get; set; }

        public int InnerBarHeightSide13 { get; set; }
        public int InnerBarHeightSide24 { get; set; }
        public int InnerBarWidthSide13 { get; set; }
        public int InnerBarWidthSide24 { get; set; }
        public int InnerBarQuantitySide13 { get; set; }
        public int InnerBarQuantitySide24 { get; set; }

        public int CornerBarHeight { get; set; }
        public int CornerBarWidth { get; set; }

        public int SubBarHeightSide13 { get; set; }
        public int SubBarWidthSide13 { get; set; }
        public int SubBarHeightSide24 { get; set; }
        public int SubBarWidthSide24 { get; set; }

        public double InnerCornerSubBarMetalRequired { get; set; }
        public double MetalRequired { get; set; }

        public string KindOfShell { get; set; }
        public decimal ShellCost { get; set; }
    }
}
