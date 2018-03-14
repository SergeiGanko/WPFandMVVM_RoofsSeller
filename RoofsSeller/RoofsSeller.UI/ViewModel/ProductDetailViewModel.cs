using System.Threading.Tasks;
using Prism.Events;
using RoofsSeller.UI.View.Services;
using RoofsSeller.UI.Data.Repositories;
using RoofsSeller.Model.Entities;
using RoofsSeller.UI.Wrapper;
using Prism.Commands;
using System.Collections.ObjectModel;
using RoofsSeller.UI.Event;
using RoofsSeller.Model;
using RoofsSeller.UI.Data.Lookups;

namespace RoofsSeller.UI.ViewModel
{
    public class ProductDetailViewModel : DetailViewModelBase, IProductDetailViewModel
    {
        private ProductWrapper _product;
        private IProductRepository _productRepository;
        private IProductTypeLookupDataService _productTypeLookupDataService;
        private IProductDiscountLookupDataService _productDiscountLookupDataService;
        private IProviderLookupDataService _providerLookupDataService;
        private IProductMeasureLookupDataService _productMeasureLookupDataService;

        public ProductDetailViewModel(IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            IProductRepository productRepository,
            IProductTypeLookupDataService productTypeLookupDataService,
            IProductDiscountLookupDataService productDiscountLookupDataService,
            IProviderLookupDataService providerLookupDataService,
            IProductMeasureLookupDataService productMeasureLookupDataService) : base(eventAggregator, messageDialogService)
        {
            _productRepository = productRepository;
            _productTypeLookupDataService = productTypeLookupDataService;
            _productDiscountLookupDataService = productDiscountLookupDataService;
            _providerLookupDataService = providerLookupDataService;
            _productMeasureLookupDataService = productMeasureLookupDataService;
            
            eventAggregator.GetEvent<AfterCollectionSavedEvent>().Subscribe(AfterCollectionSaved);

            ProductTypes = new ObservableCollection<LookupItem>();
            ProductDiscounts = new ObservableCollection<LookupItem>();
            Providers = new ObservableCollection<LookupItem>();
            ProductMeasures = new ObservableCollection<LookupItem>();
        }

        public ProductWrapper Product
        {
            get { return _product; }
            private set
            {
                _product = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<LookupItem> ProductTypes { get; }

        public ObservableCollection<LookupItem> ProductDiscounts { get; }

        public ObservableCollection<LookupItem> Providers { get; }

        public ObservableCollection<LookupItem> ProductMeasures { get; }

        public override async Task LoadAsync(int productId)
        {
            var product = productId > 0
                ? await _productRepository.GetByIdAsync(productId)
                : CreateNewProduct();

            Id = productId;

            InitializeProduct(product);

            await LoadProductTypeLookupAsync();
            await LoadProductDiscountLookupAsync();
            await LoadProviderLookupAsync();
            await LoadProductMeasureLookupAsync();
        }

        protected override async void OnDeleteExecute()
        {
            var result = await MessageDialogService.ShowOkCancelDialogAsync(
                $"Вы действительно хотите удалить продукт {Product.Name}?", "Удаление");
            if (result == MessageDialogResult.Ok)
            {
                _productRepository.Remove(Product.Model);
                await _productRepository.SaveAsync();
                RaiseDetailDeletedEvent(Product.Id);
            }
        }

        protected override bool OnSaveCanExecute()
        {
            return Product != null
                && !Product.HasErrors
                && HasChanges;
        }

        protected override async void OnSaveExecute()
        {
            await _productRepository.SaveAsync();
            HasChanges = _productRepository.HasChanges();
            Id = Product.Id;
            RaiseDetailSavedEvent(Product.Id, Product.Name);
        }

        private Product CreateNewProduct()
        {
            var product = new Product()
            {
                ProductDiscountId = 1,
                ProductTypeId = 9,
                ProviderId = 1,
                ProductMeasureId = 1
            };
            _productRepository.Add(product);
            return product;
        }

        private void InitializeProduct(Product product)
        {
            Product = new ProductWrapper(product);
            Product.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _productRepository.HasChanges();
                }
                if (e.PropertyName == nameof(Product.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
                if (e.PropertyName == nameof(Product.Name))
                {
                    SetTitle();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            if (Product.Id == 0)
            {
                // little trick to trigger the validation
                Product.Name = "";
                
            }
            SetTitle();
        }

        private void SetTitle()
        {
            Title = Product.Name;
        }

        private async Task LoadProductTypeLookupAsync()
        {
            ProductTypes.Clear();
            ProductTypes.Add(new NullLookupItem { DisplayMember = " - " });
            var lookup = await _productTypeLookupDataService.GetProductTypeLookupAsync();
            foreach (var lookupItem in lookup)
            {
                ProductTypes.Add(lookupItem);
            }
        }

        private async Task LoadProductDiscountLookupAsync()
        {
            ProductDiscounts.Clear();
            ProductDiscounts.Add(new NullLookupItem { DisplayMember = " - " });
            var lookup = await _productDiscountLookupDataService.GetProductDiscountLookupAsync();
            foreach (var lookupItem in lookup)
            {
                ProductDiscounts.Add(lookupItem);
            }
        }

        private async Task LoadProviderLookupAsync()
        {
            Providers.Clear();
            Providers.Add(new NullLookupItem { DisplayMember = " - " });
            var lookup = await _providerLookupDataService.GetProviderLookupAsync();
            foreach (var lookupItem in lookup)
            {
                Providers.Add(lookupItem);
            }
        }

        private async Task LoadProductMeasureLookupAsync()
        {
            ProductMeasures.Clear();
            ProductMeasures.Add(new NullLookupItem { DisplayMember = " - " });
            var lookup = await _productMeasureLookupDataService.GetProductMeasureLookupAsync();
            foreach (var lookupItem in lookup)
            {
                ProductMeasures.Add(lookupItem);
            }
        }

        private async void AfterCollectionSaved(AfterCollectionSavedEventArgs args)
        {
            if (args.ViewModelName == nameof(ProductTypeDetailViewModel))
            {
                await LoadProductTypeLookupAsync();
            }
        }
    }
}
