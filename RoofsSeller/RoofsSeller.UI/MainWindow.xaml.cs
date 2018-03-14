using MahApps.Metro.Controls;
using RoofsSeller.UI.ViewModel;
using System.Windows;
using RoofsSeller.UI.View.Services;

namespace RoofsSeller.UI
{
    public partial class MainWindow : MetroWindow
    {
        private readonly MainViewModel _viewModel;
        protected readonly IMessageDialogService MessageDialogService;

        public MainWindow(MainViewModel viewModel,
            IMessageDialogService messageDialogService)
        {
            InitializeComponent();
            MessageDialogService = messageDialogService;
            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }
    }
}
