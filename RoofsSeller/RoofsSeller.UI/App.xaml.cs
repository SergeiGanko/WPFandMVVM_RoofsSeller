using System;
using System.Windows;
using RoofsSeller.UI.Startup;
using Autofac;

namespace RoofsSeller.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var bootstrapeer = new Bootstrapper();
            var container = bootstrapeer.Bootstrap();
            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }

        private void Application_DispatcherUnhandledException(object sender,
            System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Unexpected error occured. Please, inform the admin."
                + Environment.NewLine + e.Exception.Message, "Unexpected error");
            e.Handled = true;
        }

        
    }
}
