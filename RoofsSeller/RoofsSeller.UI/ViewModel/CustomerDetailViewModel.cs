﻿using RoofsSeller.Model.Entities;
using System.Threading.Tasks;
using Prism.Events;
using Prism.Commands;
using RoofsSeller.UI.Wrapper;
using RoofsSeller.UI.Data.Repositories;
using RoofsSeller.UI.View.Services;
using System.Collections.ObjectModel;

namespace RoofsSeller.UI.ViewModel
{
    public class CustomerDetailViewModel : DetailViewModelBase, ICustomerDetailViewModel
    {
        private ICustomerRepository _customerRepository;
        private CustomerWrapper _customer;

        public CustomerDetailViewModel(ICustomerRepository customerRepository, 
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService) : base(eventAggregator, messageDialogService)
        {
            _customerRepository = customerRepository;
        }

        public override async Task LoadAsync(int customerId)
        {
            var customer = customerId > 0
                ? await _customerRepository.GetByIdAsync(customerId)
                : CreateNewCustomer();

            Id = customerId;

            InitializeCustomer(customer);
        }

        public CustomerWrapper Customer
        {
            get { return _customer; }
            private set
            {
                _customer = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<OrderWrapper> Orders { get; }

        protected override bool OnSaveCanExecute()
        {
            return Customer!=null 
                && !Customer.HasErrors
                && HasChanges;
        }

        protected override async void OnSaveExecute()
        {
            await SaveWithOptimisticConcurrencyAsync(_customerRepository.SaveAsync,
                () =>
                {
                    HasChanges = _customerRepository.HasChanges();
                    Id = Customer.Id;
                    RaiseDetailSavedEvent(Customer.Id, Customer.Name);
                });
        }

        protected override async void OnDeleteExecute()
        {
            if (await _customerRepository.HasOrdersAsync(Customer.Id))
            {
                await MessageDialogService
                    .ShowInfoDialogAsync($"Unable to delete customer {Customer.Name} because he has at least 1 order.");
                return;
            }

            var result = await MessageDialogService.ShowOkCancelDialogAsync(
                $"Are you sure you want to delete customer {Customer.Name}?", "Deleting");
            if (result == MessageDialogResult.Ok)
            {
                _customerRepository.Remove(Customer.Model);
                await _customerRepository.SaveAsync();
                RaiseDetailDeletedEvent(Customer.Id);
            }
        }

        private Customer CreateNewCustomer()
        {
            var customer = new Customer();
            _customerRepository.Add(customer);
            return customer;
        }

        private void InitializeCustomer(Customer customer)
        {
            Customer = new CustomerWrapper(customer);
            Customer.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _customerRepository.HasChanges();
                }
                if (e.PropertyName == nameof(Customer.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
                if (e.PropertyName == nameof(Customer.Name))
                {
                    SetTitle();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            if (Customer.Id == 0)
            {
                // little trick to trigger the validation
                Customer.Name = "";
                Customer.Address = "";
                Customer.Phone = "";
            }
            SetTitle();
        }

        private void SetTitle()
        {
            Title = Customer.Name;
        }
    }
}
