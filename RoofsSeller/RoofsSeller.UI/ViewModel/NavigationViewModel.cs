using Prism.Events;
using RoofsSeller.Model;
using RoofsSeller.Model.Entities;
using RoofsSeller.UI.Data;
using RoofsSeller.UI.Data.Lookups;
using RoofsSeller.UI.Event;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoofsSeller.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private ICustomerLookupDataService _customerLookupService;
        private IProviderLookupDataService _providerLookupService;
        private IProductLookupDataService _productLookupService;
        private IOrderLookupDataService _orderLookupService;
        private IEventAggregator _eventAggregator;

        public NavigationViewModel(ICustomerLookupDataService customerLookupService,
            IProviderLookupDataService providerLookupService,
            IProductLookupDataService productLookupService,
            IOrderLookupDataService orderLookupService,
            IEventAggregator eventAggregator)
        {
            _customerLookupService = customerLookupService;
            _providerLookupService = providerLookupService;
            _productLookupService = productLookupService;
            _orderLookupService = orderLookupService;
            _eventAggregator = eventAggregator;
            Customers = new ObservableCollection<NavigationItemViewModel>();
            Providers = new ObservableCollection<NavigationItemViewModel>();
            Products = new ObservableCollection<NavigationItemViewModel>();
            Orders = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Subscribe(AfterDetailSaved);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);
        }

        public async Task LoadAsync()
        {
            var lookup = await _customerLookupService.GetCustomerLookupAsync();
            Customers.Clear();
            foreach (var item in lookup)
            {
                Customers.Add(new NavigationItemViewModel(item.Id, item.DisplayMember,
                    nameof(CustomerDetailViewModel),
                    _eventAggregator));
            }

            lookup = await _providerLookupService.GetProviderLookupAsync();
            Providers.Clear();
            foreach (var item in lookup)
            {
                Providers.Add(new NavigationItemViewModel(item.Id, item.DisplayMember,
                    nameof(ProviderDetailViewModel),
                    _eventAggregator));
            }

            lookup = await _productLookupService.GetProductLookupAsync();
            Products.Clear();
            foreach (var item in lookup)
            {
                Products.Add(new NavigationItemViewModel(item.Id, item.DisplayMember,
                    nameof(ProductDetailViewModel),
                    _eventAggregator));
            }

            lookup = await _orderLookupService.GetOrderLookupAsync();
            Orders.Clear();
            foreach (var item in lookup)
            {
                Orders.Add(new NavigationItemViewModel(item.Id, item.DisplayMember,
                    nameof(OrderDetailViewModel),
                    _eventAggregator));
            }
        }

        public ObservableCollection<NavigationItemViewModel> Customers { get; }

        public ObservableCollection<NavigationItemViewModel> Providers { get; }

        public ObservableCollection<NavigationItemViewModel> Products { get; }

        public ObservableCollection<NavigationItemViewModel> Orders { get; }

        private void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(CustomerDetailViewModel):
                    AfterDetailSaved(Customers, args);
                    break;
                case nameof(ProviderDetailViewModel):
                    AfterDetailSaved(Providers, args);
                    break;
                case nameof(ProductDetailViewModel):
                    AfterDetailSaved(Products, args);
                    break;
                case nameof(OrderDetailViewModel):
                    AfterDetailSaved(Orders, args);
                    break;
            }
        }

        private void AfterDetailSaved(ObservableCollection<NavigationItemViewModel> items,
            AfterDetailSavedEventArgs args)
        {
            var lookupItem = items.SingleOrDefault(l => l.Id == args.Id);
            if (lookupItem == null)
            {
                items.Add(new NavigationItemViewModel(args.Id, args.DisplayMember,
                    args.ViewModelName,
                    _eventAggregator));
            }
            else
            {
                lookupItem.DisplayMember = args.DisplayMember;
            }
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(CustomerDetailViewModel):
                    AfterDetailDeleted(Customers, args);
                    break;
                case nameof(ProviderDetailViewModel):
                    AfterDetailDeleted(Providers, args);
                    break;
                case nameof(ProductDetailViewModel):
                    AfterDetailDeleted(Products, args);
                    break;
                case nameof(OrderDetailViewModel):
                    AfterDetailDeleted(Orders, args);
                    break;
            }
        }

        private void AfterDetailDeleted(ObservableCollection<NavigationItemViewModel> items,
            AfterDetailDeletedEventArgs args)
        {
            var item = items.SingleOrDefault(f => f.Id == args.Id);
            if (item != null)
            {
                items.Remove(item);
            }
        }
    }
}