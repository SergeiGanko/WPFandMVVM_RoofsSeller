using RoofsSeller.UI.Wrapper;

namespace RoofsSeller.UI.ViewModel.UmbrellaTypesViewModel
{
    public class StandartUmbrellaViewModel
    {
        public StandartUmbrellaViewModel(PipeWrapper pipe, PipeUmbrellaWrapper pipeUmbrella)
        {
            Pipe = pipe;
            PipeUmbrella = pipeUmbrella;
        }

        public PipeWrapper Pipe { get; set; }
        public PipeUmbrellaWrapper PipeUmbrella { get; set; }
    }
}
