namespace RoofsSeller.UI.ViewModel.Abutment
{
    using RoofsSeller.UI.Wrapper;

    public class AbutmentViewModel
    {
        public AbutmentViewModel(PipeAbutmentWrapper pipeAbutment)
        {
            this.PipeAbutment = pipeAbutment;
        }

        public PipeAbutmentWrapper PipeAbutment { get; set; }
    }
}
