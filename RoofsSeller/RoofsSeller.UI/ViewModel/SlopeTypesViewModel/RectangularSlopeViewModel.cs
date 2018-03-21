namespace RoofsSeller.UI.ViewModel.SlopeTypesViewModel
{
    using RoofsSeller.UI.Wrapper;

    public class RectangularSlopeViewModel
    {
        public RectangularSlopeViewModel(SlopeWrapper slope)
        {
            SlopeWrapper = slope;
        }

        public SlopeWrapper SlopeWrapper { get; set; }
    }
}
