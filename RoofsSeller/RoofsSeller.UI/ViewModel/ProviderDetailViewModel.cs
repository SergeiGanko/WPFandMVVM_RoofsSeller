using Prism.Events;
using RoofsSeller.UI.View.Services;
using RoofsSeller.UI.Data.Repositories;
using RoofsSeller.UI.Wrapper;
using RoofsSeller.Model.Entities;
using Prism.Commands;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace RoofsSeller.UI.ViewModel
{
    public class ProviderDetailViewModel : DetailViewModelBase, IProviderDetailViewModel
    {
        private IProviderRepository _providerRepository;
        private ProviderWrapper _provider;

        public ProviderDetailViewModel(IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            IProviderRepository providerRepository) : base(eventAggregator, messageDialogService)
        {
            _providerRepository = providerRepository;

            Products = new ObservableCollection<ProductWrapper>();
        }

        public ProviderWrapper Provider
        {
            get { return _provider; }
            private set
            {
                _provider = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ProductWrapper> Products { get; }

        public override async Task LoadAsync(int providerId)
        {
            var provider = providerId > 0
                ? await _providerRepository.GetByIdAsync(providerId)
                : CreateNewProvider();

            Id = providerId;

            InitializeProvider(provider);
        }

        protected override async void OnDeleteExecute()
        {
            var result = await MessageDialogService.ShowOkCancelDialogAsync(
                $"Вы действительно хотите удалить поставщика {Provider.Name}?", "Удаление");
            if (result == MessageDialogResult.Ok)
            {
                _providerRepository.Remove(Provider.Model);
                await _providerRepository.SaveAsync();
                RaiseDetailDeletedEvent(Provider.Id);
            }
        }

        protected override bool OnSaveCanExecute()
        {
            return Provider != null
                && !Provider.HasErrors
                && HasChanges;
        }

        // Логика для SaveCommand
        protected override async void OnSaveExecute()
        {
            await _providerRepository.SaveAsync();
            HasChanges = _providerRepository.HasChanges();
            Id = Provider.Id;
            RaiseDetailSavedEvent(Provider.Id, Provider.Name);
        }

        private Provider CreateNewProvider()
        {
            var provider = new Provider();
            
            _providerRepository.Add(provider);
            return provider;
        }

        private void InitializeProvider(Provider provider)
        {
            Provider = new ProviderWrapper(provider);
            Provider.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _providerRepository.HasChanges();
                }

                if (e.PropertyName == nameof(Provider.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
                if (e.PropertyName == nameof(Provider.Name))
                {
                    SetTitle();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            if (Provider.Id == 0)
            {
                // little trick to trigger the validation
                Provider.Name = "";
                Provider.Address = "";
                Provider.Phone = "";
            }
            SetTitle();
        }

        private void SetTitle()
        {
            Title = Provider.Name;
        }
    }
}
