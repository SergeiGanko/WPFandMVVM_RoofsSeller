using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using RoofsSeller.UI.View.Services;
using RoofsSeller.UI.Data.Repositories;
using System.Collections.ObjectModel;
using RoofsSeller.UI.Wrapper;
using Prism.Commands;
using System.ComponentModel;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using RoofsSeller.UI.Data.Lookups;
using RoofsSeller.Model;
using System.Windows.Input;
using RoofsSeller.Model.Entities;

namespace RoofsSeller.UI.ViewModel
{
    public class StocksStatisticDetailViewModel : DetailViewModelBase
    {
        private IProductRepository _productRepository;
        private IProductTypeLookupDataService _productTypeLookupDataService;
        private Product _product;

        public StocksStatisticDetailViewModel(IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            IProductRepository productRepository,
            IProductTypeLookupDataService productTypeLookupDataService) : base(eventAggregator, messageDialogService)
        {
            _productRepository = productRepository;
            _productTypeLookupDataService = productTypeLookupDataService;
            Title = "Остатки на складе";

            Products = new ObservableCollection<Product>();
            ProductTypes = new ObservableCollection<LookupItem>();

            SeriesCollection = new SeriesCollection();
            //Formatter = value => value.ToString("N");
            Labels = new List<string>();

            ShowStatCommand = new DelegateCommand(OnShowStatExecute);
        }

        public override async Task LoadAsync(int productId)
        {
            Id = productId;

            Products.Clear();

            var products = await _productRepository.GetAllProductsAsync();

            await LoadProductTypeLookupAsync();

            foreach (var product in products)
            {
                Products.Add(product);

                if (product.ProductTypeId == 1)
                {
                    SeriesCollection.Add(new ColumnSeries
                    {
                        Title = product.Name,
                        Values = new ChartValues<int> { product.StockBalance }
                    });
                }
                Labels.Add(product.ProductType.Type);
            }
        }

        public SeriesCollection SeriesCollection { get; }
        public List<string> Labels { get; }
        public Func<double, string> Formatter { get; }

        public ObservableCollection<LookupItem> ProductTypes { get; }

        public ObservableCollection<Product> Products { get; }

        public Product Product
        {
            get { return _product; }
            set
            {
                _product = value;
                OnPropertyChanged();
            }
        }


        public ICommand ShowStatCommand { get; }

        protected override void OnDeleteExecute()
        {
            throw new NotImplementedException();
        }

        protected override bool OnSaveCanExecute()
        {
            throw new NotImplementedException();
        }

        protected override void OnSaveExecute()
        {
            throw new NotImplementedException();
        }

        private async Task LoadProductTypeLookupAsync()
        {
            ProductTypes.Clear();
            //ProductTypes.Add(new NullLookupItem { DisplayMember = " - " });
            var lookup = await _productTypeLookupDataService.GetProductTypeLookupAsync();
            foreach (var lookupItem in lookup)
            {
                ProductTypes.Add(lookupItem);
            }
        }

        private void OnShowStatExecute()
        {
            throw new NotImplementedException();
        }
    }
}
