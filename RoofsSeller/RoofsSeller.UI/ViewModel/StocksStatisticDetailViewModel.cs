using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Events;
using RoofsSeller.UI.View.Services;
using RoofsSeller.UI.Data.Repositories;
using System.Collections.ObjectModel;
using Prism.Commands;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Input;
using RoofsSeller.Model.Entities;

namespace RoofsSeller.UI.ViewModel
{
    public class StocksStatisticDetailViewModel : DetailViewModelBase
    {
        private IProductRepository _productRepository;
        private Product _product;
        private ProductType _selectedProductType;
        private IProductTypeRepository _productTypeRepository;
        private ProductMeasure _selectedProductMeasure;

        public StocksStatisticDetailViewModel(IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            IProductRepository productRepository,
            IProductTypeRepository productTypeRepository) : base(eventAggregator, messageDialogService)
        {
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
            Title = "Остатки на складе";

            Products = new ObservableCollection<Product>();
            ProductTypes = new ObservableCollection<ProductType>();
            SeriesCollection = new SeriesCollection();
            //Formatter = value => value.ToString("N");
            Labels = new List<string>();

            ShowStatCommand = new DelegateCommand(OnShowStatExecute);
        }

        public SeriesCollection SeriesCollection { get; }
        public List<string> Labels { get; }
        public Func<double, string> Formatter { get; }

        public ObservableCollection<ProductType> ProductTypes { get; }

        public ProductType SelectedProductType
        {
            get { return _selectedProductType; }
            set
            {
                _selectedProductType = value;
                OnPropertyChanged();
            }
        }

        //public ObservableCollection<ProductMeasure> ProductMeasures { get; }

        public ProductMeasure SelectedProductMeasure
        {
            get { return _selectedProductMeasure; }
            set
            {
                _selectedProductMeasure = value;
                OnPropertyChanged();
            }
        }

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

        public override async Task LoadAsync(int productId)
        {
            Id = productId;

            Products.Clear();

            var products = await _productRepository.GetAllProductsAsync();

            foreach (var product in products)
            {
                Products.Add(product);
            }

            var prodTypes = await _productTypeRepository.GetAllProductTypesAsync();

            foreach (var prodType in prodTypes)
            {
                ProductTypes.Add(prodType);
            }

            SelectedProductType = ProductTypes.First();
            Product = Products.First(s => s.ProductTypeId == SelectedProductType.Id);
            SelectedProductMeasure = Product.ProductMeasure;

            foreach (var product in Products)
            {
                if (product.ProductTypeId == SelectedProductType.Id)
                {
                    SeriesCollection.Add(new ColumnSeries
                    {
                        Title = product.Name,
                        Values = new ChartValues<int> { product.StockBalance }
                    });
                    Labels.Add(product.ProductType.Type);
                }
            }
        }

        private async void OnShowStatExecute()
        {
            SeriesCollection.Clear();
            Labels.Clear();

            foreach (var product in Products)
            {
                if (product.ProductTypeId == SelectedProductType.Id)
                {
                    SeriesCollection.Add(new ColumnSeries
                    {
                        Title = product.Name,
                        Values = new ChartValues<int> { product.StockBalance }
                    });
                    Labels.Add(product.ProductType.Type);
                }
            }

            Product = Products.FirstOrDefault(s => s.ProductTypeId == SelectedProductType.Id);
            if (Product != null)
            {
                SelectedProductMeasure = Product.ProductMeasure;
            }
            else
            {
                await MessageDialogService.ShowInfoDialogAsync(
                        "Продуктов указанного типа нет на складе");
            }
        }

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
    }
}
