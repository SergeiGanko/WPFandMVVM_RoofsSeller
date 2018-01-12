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

namespace RoofsSeller.UI.ViewModel
{
    // А надо ли нам такой дитэйл вию модел??????
    public class OrderStateDetailViewModel : DetailViewModelBase
    {
        private IOrderStateRepository _orderStateRepository;

        public OrderStateDetailViewModel(IEventAggregator eventAggregator, 
            IMessageDialogService messageDialogService,
            IOrderStateRepository orderStateRepository) : base(eventAggregator, messageDialogService)
        {
            _orderStateRepository = orderStateRepository;

            Title = "Order State";

            OrderStates = new ObservableCollection<OrderStateWrapper>();
        }

        public ObservableCollection<OrderStateWrapper> OrderStates { get; }

        public override Task LoadAsync(int Id)
        {
            throw new NotImplementedException();
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
