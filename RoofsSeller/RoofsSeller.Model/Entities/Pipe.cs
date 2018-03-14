namespace RoofsSeller.Model.Entities
{
    public class Pipe
    {
        public int Id { get; set; }
        public int PipeLength { get; set; }
        public int PipeWidth { get; set; }
        public int RoofAngle { get; set; }
        public int PipeHeight1 { get; set; }
        public int PipeHeight2 { get; set; }
        public int InsulationThickness { get; set; }
        public int BarThickness { get; set; }
        public int ContrBarThickness { get; set; }
        public decimal MetalSheetCost { get; set; }
        public decimal PipeCost { get; set; }

        public PipeAbutment Abutment { get; set; }
        public PipeShell Shell { get; set; }
        public PipeUmbrella Umbrella { get; set; }
    }
}
