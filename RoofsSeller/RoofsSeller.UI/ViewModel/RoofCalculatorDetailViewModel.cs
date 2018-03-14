using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Prism.Commands;
using Prism.Events;
using RoofsSeller.Model;
using RoofsSeller.Model.Entities;
using RoofsSeller.UI.Data.Lookups;
using RoofsSeller.UI.View.Services;
using RoofsSeller.UI.Wrapper;

namespace RoofsSeller.UI.ViewModel
{
    public class RoofCalculatorDetailViewModel : DetailViewModelBase
    {
        private readonly IProductLookupDataService _productLookupDataService;
        private SlopeWrapper _slope;

        public RoofCalculatorDetailViewModel(IEventAggregator eventAggregator,
            IProductLookupDataService productLookupDataService,
            IMessageDialogService messageDialogService)
            : base(eventAggregator, messageDialogService)
        {
            _productLookupDataService = productLookupDataService;

            Title = "Калькулятор";

            Roofing = new ObservableCollection<LookupItem>();

            SlopeTypes = new ObservableCollection<PicClass>
            {
                new PicClass
                {
                    PicImage = new BitmapImage(new Uri("pack://application:,,,/Images/rectangle.PNG")),
                    Name = "Прямоугольник"
                },
                new PicClass
                {
                    PicImage = new BitmapImage(new Uri("pack://application:,,,/Images/triangle.PNG")),
                    Name = "Треугольник"
                },
                new PicClass
                {
                    PicImage = new BitmapImage(new Uri("pack://application:,,,/Images/trapez.PNG")),
                    Name = "Трапеция"
                },
                new PicClass
                {
                    PicImage = new BitmapImage(new Uri("pack://application:,,,/Images/parallel.PNG")),
                    Name = "Параллелограмм"
                }
            };

            AddCommand = new DelegateCommand(OnAddExecute);
            RemoveCommand = new DelegateCommand(OnRemoveExecute, OnRemoveCanExecute);
        }

        public ObservableCollection<LookupItem> Roofing { get; }

        public ObservableCollection<PicClass> SlopeTypes { get; }

        public SlopeWrapper Slope
        {
            get { return _slope; }
            set
            {
                _slope = value;
                OnPropertyChanged();
            }
        }


        public ICommand AddCommand { get; }

        public ICommand RemoveCommand { get; }

        private object _selectedViewModel;
        
        public object SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged();
            }
        }

        public override async Task LoadAsync(int id)
        {
            Id = id;

            var slope = CreateNewSlope();

            InitializeSlope(slope);

            await LoadProductLookupAsync();
        }

        private void InitializeSlope(Slope slope)
        {
            Slope = new SlopeWrapper(slope);
        }

        private Slope CreateNewSlope()
        {
            var slope = new Slope();
            return slope;
        }

        private async Task LoadProductLookupAsync()
        {
            Roofing.Clear();
            //Metals.Add(new NullLookupItem { DisplayMember = " - " });
            var lookup = await _productLookupDataService.GetRoofingLookupAsync();
            foreach (var lookupItem in lookup)
            {
                Roofing.Add(lookupItem);
            }
        }


        private bool OnRemoveCanExecute()
        {
            // ?????????? SelectedSlope at DataGrid
            return SelectedViewModel != null;
        }

        private void OnRemoveExecute()
        {
            throw new NotImplementedException();
        }

        private void OnAddExecute()
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

        protected override void OnDeleteExecute()
        {
            throw new NotImplementedException();
        }
    }
}
