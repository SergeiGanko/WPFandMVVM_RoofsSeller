using RoofsSeller.UI.Wrapper;

namespace RoofsSeller.UI.ViewModel.ShellTypesViewModel
{
    public class ExclusiveShellViewModel
    {
        public ExclusiveShellViewModel(PipeWrapper pipe, PipeShellWrapper pipeShell)
        {
            Pipe = pipe;
            PipeShell = pipeShell;
        }

        public PipeWrapper Pipe { get; set; }

        public PipeShellWrapper PipeShell { get; set; }
    }
}
