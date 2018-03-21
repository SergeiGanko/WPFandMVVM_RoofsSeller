namespace RoofsSeller.UI.ViewModel.SlopeTypesViewModel
{
    using RoofsSeller.UI.Wrapper;

    public class ParallelogramSlopeViewModel
    {
        public ParallelogramSlopeViewModel(SlopeWrapper slope)
        {
            SlopeWrapper = slope;
        }

        public SlopeWrapper SlopeWrapper { get; set; }
    }
}
