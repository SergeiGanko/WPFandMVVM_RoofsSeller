using RoofsSeller.UI.Wrapper;

namespace RoofsSeller.UI.ViewModel.UmbrellaTypesViewModel
{
    public class EconomUmbrellaViewModel
    {
        public EconomUmbrellaViewModel(PipeWrapper pipe, PipeUmbrellaWrapper pipeUmbrella)
        {
            Pipe = pipe;
            PipeUmbrella = pipeUmbrella;
        }

        public PipeWrapper Pipe { get; set; }
        public PipeUmbrellaWrapper PipeUmbrella { get; set; }
    }
}
