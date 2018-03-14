namespace RoofsSeller.Model.Entities
{
    public class PipeAbutment
    {
        public int SideWidth { get; set; }
        public int SideLength { get; set; }
        public int FrontWidth { get; set; }
        public int FrontLength { get; set; }
        public int BackWidth { get; set; }
        public int BackLength { get; set; }
        public int MetalSheetQuantityRequired { get; set; }
        public decimal AbutmentCost { get; set; }
    }
}
