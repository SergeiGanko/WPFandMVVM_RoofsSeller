namespace RoofsSeller.UI.ViewModel.SlopeTypesViewModel
{
    using RoofsSeller.UI.Wrapper;

    public class TriangularSlopeViewModel
    {
        public TriangularSlopeViewModel(SlopeWrapper slope)
        {
            SlopeWrapper = slope;
        }

        public SlopeWrapper SlopeWrapper { get; set; }
    }
}
