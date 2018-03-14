namespace RoofsSeller.Model.Entities
{
    public class PipeUmbrella
    {
        public string KindOfUmbrella { get; set; }

        public int FitWidthSide13 { get; set; }
        public int FitLengthSide13 { get; set; }
        public int FitQuantitySide13 { get; set; }
        public int FitWidthSide24 { get; set; }
        public int FitLengthSide24 { get; set; }
        public int FitQuantitySide24 { get; set; }

        public int LegWidth { get; set; }
        public int LegHeight { get; set; }
        public int LegQuantity { get; set; }

        public int SubBarWidth { get; set; }
        public int SubBarHeight { get; set; }
        public int SubBarQuantity { get; set; }

        public int BottomPartWidth { get; set; }
        public int BottomPartLength { get; set; }

        public int ValmaWidth { get; set; }
        public int ValmaLength { get; set; }

        public int TriangleWidth { get; set; }
        public int TriangleLength { get; set; }

        public int LouversLengthSide13 { get; set; }
        public int LouversWidthSide13 { get; set; }
        public int LouversLengthSide24 { get; set; }
        public int LouversWidthSide24 { get; set; }

        public double MetalRequired { get; set; }

        public decimal UmbrellaCost { get; set; }
    }
}
