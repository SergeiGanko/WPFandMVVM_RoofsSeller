using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using RoofsSeller.UI.Data.Repositories;
using RoofsSeller.UI.View.Services;
using RoofsSeller.UI.Wrapper;
using System.Collections.ObjectModel;
using RoofsSeller.Model.Entities;
using Prism.Commands;
using RoofsSeller.UI.Data.Lookups;
using RoofsSeller.Model;
using System.Windows.Input;
using System.ComponentModel;
using RoofsSeller.UI.Event;

namespace RoofsSeller.UI.ViewModel
{
    public class OrderDetailViewModel : DetailViewModelBase, IOrderDetailViewModel
    {
        private OrderWrapper _order;
        private OrderItemWrapper _selectedOrderItem;
        private IOrderRepository _orderRepository;
        private Product _selectedAvailableProduct;
        private Product _selectedAddedProduct;

        private ICustomerLookupDataService _customerLookupDataService;
        private IOrderStateLookupDataService _orderStateLookupDataService;
        private IProductTypeLookupDataService _productTypeLookupDataService;
        private List<Product> _allProducts;

        public OrderDetailViewModel(IEventAggregator eventAggregator,
            IOrderRepository orderRepository,
            IMessageDialogService messageDialogService,
            ICustomerLookupDataService customerLookupDataService,
            IOrderStateLookupDataService orderStateLookupDataService,
            IProductTypeLookupDataService productTypeLookupDataService) : base(eventAggregator, messageDialogService)
        {
            _orderRepository = orderRepository;
            _customerLookupDataService = customerLookupDataService;
            _orderStateLookupDataService = orderStateLookupDataService;
            _productTypeLookupDataService = productTypeLookupDataService;

            eventAggregator.GetEvent<AfterDetailSavedEvent>().Subscribe(AfterDetailSaved);
            eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);

            AddedProducts = new ObservableCollection<Product>();
            AvailableProducts = new ObservableCollection<Product>();
            AddProductCommand = new DelegateCommand(OnAddProductExecute, OnAddProductCanExecute);
            RemoveProductCommand = new DelegateCommand(OnRemoveProductExecute, OnRemoveProductCanExecute);

            OrderItems = new ObservableCollection<OrderItemWrapper>();
            Customers = new ObservableCollection<LookupItem>();
            OrderStates = new ObservableCollection<LookupItem>();
            ProductTypes = new ObservableCollection<LookupItem>();
        }

        public OrderWrapper Order
        {
            get { return _order; }
            set {
                _order = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddProductCommand { get; }

        public ICommand RemoveProductCommand { get; }

        public ObservableCollection<Product> AddedProducts { get; }

        public ObservableCollection<Product> AvailableProducts { get; }

        public Product SelectedAvailableProduct
        {
            get { return _selectedAvailableProduct; }
            set
            {
                _selectedAvailableProduct = value;
                OnPropertyChanged();
                ((DelegateCommand)AddProductCommand).RaiseCanExecuteChanged();
            }
        }

        public Product SelectedAddedProduct
        {
            get { return _selectedAddedProduct; }
            set
            {
                _selectedAddedProduct = value;
                OnPropertyChanged();
                ((DelegateCommand)RemoveProductCommand).RaiseCanExecuteChanged();
            }
        }

        public OrderItemWrapper SelectedOrderItem
        {
            get { return _selectedOrderItem; }
            set
            {
                _selectedOrderItem = value;
                OnPropertyChanged();
            }
        }

        //public ICommand AddOrderItemCommand { get; }

        //public ICommand RemoveOrderItemCommand { get; }

        public ObservableCollection<OrderItemWrapper> OrderItems { get; }

        public ObservableCollection<LookupItem> ProductTypes { get; }

        public ObservableCollection<LookupItem> Customers { get; }

        public ObservableCollection<LookupItem> OrderStates { get; }

        public override async Task LoadAsync(int orderId)
        {
            var order = orderId > 0
                ? await _orderRepository.GetByIdAsync(orderId)
                : await CreateNewOrder();

            Id = orderId;

            InitializeOrder(order);

            InitializeOrderItems(order.OrderItems);

            await LoadCustomerLookupAsync();
            await LoadOrderStateLookupAsync();

            _allProducts = await _orderRepository.GetAllProductsAsync();

            SetupPicklist();
        }

        protected override async void OnDeleteExecute()
        {
            var result = await MessageDialogService.ShowOkCancelDialogAsync(
                $"Вы действительно хотите удалить заказ №{Order.OrderNumber}?", "Удаление");
            if (result == MessageDialogResult.Ok)
            {
                _orderRepository.Remove(Order.Model);
                await _orderRepository.SaveAsync();
                RaiseDetailDeletedEvent(Order.Id);
            }
        }

        protected override bool OnSaveCanExecute()
        {
            return Order != null
                && !Order.HasErrors
                && HasChanges;
        }

        protected override async void OnSaveExecute()
        {
            await _orderRepository.SaveAsync();
            HasChanges = _orderRepository.HasChanges();
            Id = Order.Id;
            RaiseDetailSavedEvent(Order.Id, 
                $"Заказ №{Order.OrderNumber} от {Order.OrderDate.ToShortDateString()}");
        }

        private async Task<Order> CreateNewOrder()
        {
            Order lastOrder = await _orderRepository.GetLastOrderAsync();

            var order = new Order()
            {
                OrderDate = DateTime.Now.Date,
                OrderNumber = lastOrder.OrderNumber + 1,
                StateId = 1,
                ShippingDate = DateTime.Now.Date
            };
            _orderRepository.Add(order);
            return order;
        }

        private void InitializeOrder(Order order)
        {
            Order = new OrderWrapper(order);
            Order.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _orderRepository.HasChanges();
                }
                if (e.PropertyName == nameof(Order.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
                if (e.PropertyName == nameof(Order.OrderNumber)
                    || e.PropertyName == nameof(Order.OrderDate))
                {
                    SetTitle();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            if (Order.Id == 0)
            {
                // little trick to trigger the validation
                Order.OrderDate = DateTime.Now.Date;
            }
            SetTitle();
        }

        private void SetTitle()
        {
            Title = $"Заказ №{Order.OrderNumber} от {Order.OrderDate.ToShortDateString()}";
        }


        private void InitializeOrderItems(ICollection<OrderItem> orderItems)
        {
            foreach (var wrapper in OrderItems)
            {
                wrapper.PropertyChanged -= OrderItemWrapper_PropertyChanged;
            }
            OrderItems.Clear();
            foreach (var orderItem in orderItems)
            {
                var wrapper = new OrderItemWrapper(orderItem);
                OrderItems.Add(wrapper);
                wrapper.PropertyChanged += OrderItemWrapper_PropertyChanged;
            }
        }

        private void OrderItemWrapper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _orderRepository.HasChanges();
            }
            if (e.PropertyName == nameof(OrderItemWrapper.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        private async Task LoadOrderStateLookupAsync()
        {
            OrderStates.Clear();
            var lookup = await _orderStateLookupDataService.GetOrderStateLookupAsync();
            foreach (var lookupItem in lookup)
            {
                OrderStates.Add(lookupItem);
            }
        }

        private async Task LoadCustomerLookupAsync()
        {
            Customers.Clear();
            Customers.Add(new NullLookupItem { DisplayMember = " - " });
            var lookup = await _customerLookupDataService.GetCustomerLookupAsync();
            foreach (var lookupItem in lookup)
            {
                Customers.Add(lookupItem);
            }
        }

        //private void InitializeOrderItems(ICollection<OrderItem> orderItems)
        //{
        //    foreach (var wrapper in OrderItems)
        //    {
        //        wrapper.PropertyChanged -= OrderItemWrapper_PropertyChanged;
        //    }
        //    OrderItems.Clear();
        //    foreach (var orderItem in orderItems)
        //    {
        //        var wrapper = new OrderItemWrapper(orderItem);
        //        OrderItems.Add(wrapper);
        //        wrapper.PropertyChanged += OrderItemWrapper_PropertyChanged;
        //    }
        //}

        //private void OrderItemWrapper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    if (!HasChanges)
        //    {
        //        HasChanges = _orderRepository.HasChanges();
        //    }
        //    if (e.PropertyName == nameof(OrderItemWrapper.HasErrors))
        //    {
        //        ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        //    }
        //}

        //private void OnAddOrderItemExecute()
        //{
        //    var newItem = new OrderItemWrapper(new OrderItem());

        //    newItem.PropertyChanged += OrderItemWrapper_PropertyChanged;
        //    OrderItems.Add(newItem);
        //    Order.Model.OrderItems.Add(newItem.Model);
        //}

        //private bool OnRemoveOrderItemCanExecute()
        //{
        //    return SelectedOrderItem != null;
        //}

        //private void OnRemoveOrderItemExecute()
        //{
        //    SelectedOrderItem.PropertyChanged -= OrderItemWrapper_PropertyChanged;
        //    _orderRepository.RemoveOrderItem(SelectedOrderItem.Model);
        //    OrderItems.Remove(SelectedOrderItem);
        //    SelectedOrderItem = null;
        //    HasChanges = _orderRepository.HasChanges();
        //    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        //}

        // ???????????????????????
        private int _productQuantityToAdd;

        public int ProductQuantityToAdd
        {
            get { return _productQuantityToAdd; }
            set
            {
                _productQuantityToAdd = value;
                if (SelectedAvailableProduct.StockBalance < _productQuantityToAdd)
                {
                    _productQuantityToAdd = SelectedAvailableProduct.StockBalance;
                    // TODO: выдавать предупреждение.
                }
                OnPropertyChanged();
            }
        }

        private void OnAddProductExecute()
        {
            var productToAdd = SelectedAvailableProduct;
            decimal discount = (decimal)(100 - productToAdd.ProductDiscount.Rate) / 100;

            Order.Model.OrderItems.Add(new OrderItem
            {
                OrderId = Id,
                ProductId = productToAdd.Id,
                UnitPrice = productToAdd.Price,
                Quantity =  ProductQuantityToAdd,
                TotalPrice = ProductQuantityToAdd * productToAdd.Price * discount
            });

            SelectedAvailableProduct.StockBalance -= ProductQuantityToAdd;
            Order.TotalAmount += 
                ProductQuantityToAdd * productToAdd.Price * discount;

            AddedProducts.Add(productToAdd);
            AvailableProducts.Remove(productToAdd);
            HasChanges = _orderRepository.HasChanges();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private bool OnRemoveProductCanExecute()
        {
            return SelectedAddedProduct != null;
        }

        private void OnRemoveProductExecute()
        {
            var productToRemove = SelectedAddedProduct;
            OrderItem orderItemToRemove = productToRemove.OrderItems.FirstOrDefault(o => o.ProductId == productToRemove.Id);

            Order.Model.OrderItems.Remove(orderItemToRemove);

            productToRemove.StockBalance += orderItemToRemove.Quantity;

            decimal discount = (decimal)(100 - productToRemove.ProductDiscount.Rate) / 100;
            Order.TotalAmount -= orderItemToRemove.Quantity * orderItemToRemove.UnitPrice * discount;

            AddedProducts.Remove(productToRemove);
            AvailableProducts.Add(productToRemove);
            HasChanges = _orderRepository.HasChanges();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private bool OnAddProductCanExecute()
        {
            return SelectedAvailableProduct != null;
        }

        private void SetupPicklist()
        {
            var orderProductsIds = Order.Model.OrderItems.Select(o => o.Product.Id).ToList();
            var addedProducts = _allProducts.Where(p => orderProductsIds.Contains(p.Id)).OrderBy(o => o.Name);
            var availableProducts = _allProducts.Except(addedProducts).OrderBy(p => p.Name);

            AddedProducts.Clear();
            AvailableProducts.Clear();
            foreach (var addedProduct in addedProducts)
            {
                AddedProducts.Add(addedProduct);
            }
            foreach (var availableProduct in availableProducts)
            {
                AvailableProducts.Add(availableProduct);
            }
        }

        private async void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
            if (args.ViewModelName == nameof(OrderDetailViewModel))
            {
                await _orderRepository.ReloadProductAsync(args.Id);
                _allProducts = await _orderRepository.GetAllProductsAsync();
                SetupPicklist();
                var order = await _orderRepository.GetByIdAsync(args.Id);
                InitializeOrderItems(order.OrderItems);
            }
        }

        private async void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            if (args.ViewModelName == nameof(OrderDetailViewModel))
            {
                _allProducts = await _orderRepository.GetAllProductsAsync();
                SetupPicklist();
                var order = await _orderRepository.GetByIdAsync(args.Id);
                InitializeOrderItems(order.OrderItems);
            }
        }
    }
}
