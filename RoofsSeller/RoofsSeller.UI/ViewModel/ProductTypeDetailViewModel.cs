using System;
using System.Linq;
using System.Threading.Tasks;
using Prism.Events;
using RoofsSeller.UI.View.Services;
using RoofsSeller.UI.Data.Repositories;
using System.Collections.ObjectModel;
using RoofsSeller.UI.Wrapper;
using System.ComponentModel;
using Prism.Commands;
using System.Windows.Input;
using RoofsSeller.Model.Entities;

namespace RoofsSeller.UI.ViewModel
{
    internal sealed class ProductTypeDetailViewModel : DetailViewModelBase
    {
        private readonly IProductTypeRepository _productTypeRepository;
        private ProductTypeWrapper _selectedProductType;

        public ProductTypeDetailViewModel(IEventAggregator eventAggregator, 
            IMessageDialogService messageDialogService,
            IProductTypeRepository productTypeRepository) : base(eventAggregator, messageDialogService)
        {
            _productTypeRepository = productTypeRepository;
            Title = "Типы продуктов";

            ProductTypes = new ObservableCollection<ProductTypeWrapper>();

            AddCommand = new DelegateCommand(OnAddExecute);
            RemoveCommand = new DelegateCommand(OnRemoveExecute, OnRemoveCanExecute);
        }

        public override async Task LoadAsync(int productTypeId)
        {
            
            Id = productTypeId;

            foreach (var wrapper in ProductTypes)
            {
                wrapper.PropertyChanged -= Wrapper_PropertyChanged;
            }

            ProductTypes.Clear();

            var types = await _productTypeRepository.GetAllAsync();

            foreach (var model in types)
            {
                var wrapper = new ProductTypeWrapper(model);
                wrapper.PropertyChanged += Wrapper_PropertyChanged;
                ProductTypes.Add(wrapper);
            }
        }

        private void Wrapper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _productTypeRepository.HasChanges();
            }
            if (e.PropertyName == nameof(ProductTypeWrapper.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<ProductTypeWrapper> ProductTypes { get; }

        public ICommand RemoveCommand { get; }

        public ICommand AddCommand { get; }

        public ProductTypeWrapper SelectedProductType
        {
            get { return _selectedProductType; }
            set
            {
                _selectedProductType = value;
                OnPropertyChanged();
                ((DelegateCommand)RemoveCommand).RaiseCanExecuteChanged();
            }
        }

        protected override void OnDeleteExecute()
        {
            throw new NotImplementedException();
        }

        protected override bool OnSaveCanExecute()
        {
            return HasChanges && ProductTypes.All(p => !p.HasErrors);
        }

        protected override async void OnSaveExecute()
        {
            try
            {
                await _productTypeRepository.SaveAsync();
                HasChanges = _productTypeRepository.HasChanges();
                RaiseCollectionSavedEvent();
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                await MessageDialogService.ShowInfoDialogAsync(
                    "Ошибка сохранения сущности, данные будут перезагружены. Подробности: " + ex.Message);
                await LoadAsync(Id);
            }
        }

        private void OnAddExecute()
        {
            var wrapper = new ProductTypeWrapper(new ProductType());
            wrapper.PropertyChanged += Wrapper_PropertyChanged;
            _productTypeRepository.Add(wrapper.Model);
            ProductTypes.Add(wrapper);

            // Triiger the validation
            wrapper.Type = "";
        }

        private bool OnRemoveCanExecute()
        {
            return SelectedProductType != null;
        }

        private async void OnRemoveExecute()
        {
            var isReferenced = 
                await _productTypeRepository.IsReferencedByProductAsync(SelectedProductType.Id);
            if (isReferenced)
            {
                await MessageDialogService.ShowInfoDialogAsync(
                    $"Тип {SelectedProductType.Type} не может быть удален, т.к. связан как минимум с одним товаром");
                return;
            }

            SelectedProductType.PropertyChanged -= Wrapper_PropertyChanged;
            _productTypeRepository.Remove(SelectedProductType.Model);
            ProductTypes.Remove(SelectedProductType);
            SelectedProductType = null;
            HasChanges = _productTypeRepository.HasChanges();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }
    }
}
