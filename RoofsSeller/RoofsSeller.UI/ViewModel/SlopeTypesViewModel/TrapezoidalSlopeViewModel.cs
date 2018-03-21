using RoofsSeller.UI.Wrapper;

namespace RoofsSeller.UI.ViewModel.SlopeTypesViewModel
{
    public class TrapezoidalSlopeViewModel
    {
        public TrapezoidalSlopeViewModel(SlopeWrapper slope)
        {
            SlopeWrapper = slope;
        }

        public SlopeWrapper SlopeWrapper { get; set; }
    }
}
