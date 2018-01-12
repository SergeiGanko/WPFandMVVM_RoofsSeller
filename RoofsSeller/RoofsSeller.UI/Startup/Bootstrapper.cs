using Autofac;
using RoofsSeller.UI.ViewModel;
using RoofsSeller.DataAccess;
using Prism.Events;
using RoofsSeller.UI.Data.Lookups;
using RoofsSeller.UI.Data.Repositories;
using RoofsSeller.UI.View.Services;

namespace RoofsSeller.UI.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterType<RoofsSellerDbContext>().AsSelf();

            builder.RegisterType<MainWindow>().AsSelf();

            builder.RegisterType<MessageDialogService>().As<IMessageDialogService>();

            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<CustomerDetailViewModel>().Keyed<IDetailViewModel>(nameof(CustomerDetailViewModel));
            builder.RegisterType<ProductDetailViewModel>().Keyed<IDetailViewModel>(nameof(ProductDetailViewModel));
            builder.RegisterType<ProviderDetailViewModel>().Keyed<IDetailViewModel>(nameof(ProviderDetailViewModel));
            builder.RegisterType<OrderDetailViewModel>().Keyed<IDetailViewModel>(nameof(OrderDetailViewModel));
            builder.RegisterType<ProductTypeDetailViewModel>().Keyed<IDetailViewModel>(nameof(ProductTypeDetailViewModel));
            builder.RegisterType<StocksStatisticDetailViewModel>().Keyed<IDetailViewModel>(nameof(StocksStatisticDetailViewModel));

            builder.RegisterType<LookupDataService>().AsImplementedInterfaces();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
            builder.RegisterType<ProviderRepository>().As<IProviderRepository>();
            builder.RegisterType<ProductRepository>().As<IProductRepository>();
            builder.RegisterType<ProductTypeRepository>().As<IProductTypeRepository>();

            // create container by method Build and return the container from these Bootstrap-method
            return builder.Build(); 
        }
    }
}
