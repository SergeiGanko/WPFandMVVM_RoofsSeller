using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using RoofsSeller.Model;
using RoofsSeller.Model.Entities;
using RoofsSeller.UI.Data.Lookups;
using RoofsSeller.UI.View.Services;
using RoofsSeller.UI.Wrapper;
using System.ComponentModel;
using RoofsSeller.UI.ViewModel.SlopeTypesViewModel;

namespace RoofsSeller.UI.ViewModel
{
    public class RoofCalculatorDetailViewModel : DetailViewModelBase
    {
        private const string ViewTitle = "Roof Calculator";
        private const string Rectangle = "Rectangle";
        private const string Triangle = "Triangle";
        private const string Trapeze = "Trapeze";
        private const string Parallelogram = "Parallelogram";

        private readonly IProductLookupDataService _productLookupDataService;
        private SlopeWrapper _slope;
        private SlopeWrapper _selectedSlope;
        private RoofWrapper _currentRoof;

        public RoofCalculatorDetailViewModel(IEventAggregator eventAggregator,
            IProductLookupDataService productLookupDataService,
            IMessageDialogService messageDialogService)
            : base(eventAggregator, messageDialogService)
        {
            _productLookupDataService = productLookupDataService;

            Title = ViewTitle;

            Roofing = new ObservableCollection<LookupItem>();

            SlopeTypes = new ObservableCollection<string> { Rectangle, Triangle, Trapeze, Parallelogram };

            ModuleEffectiveSquares = new ObservableCollection<double> { 0.770, 0.805, 0.910 };

            Slopes = new ObservableCollection<SlopeWrapper>();

            NextCommand = new DelegateCommand(OnNextExecute);
            AddCommand = new DelegateCommand(OnAddExecute);
            RemoveCommand = new DelegateCommand(OnRemoveExecute, OnRemoveCanExecute);
        }

        public ObservableCollection<LookupItem> Roofing { get; }

        public ObservableCollection<string> SlopeTypes { get; }

        public ObservableCollection<double> ModuleEffectiveSquares { get; }

        public ObservableCollection<SlopeWrapper> Slopes { get; }

        public RoofWrapper CurrentRoof
        {
            get
            {
                return _currentRoof;
            }
            set
            {
                _currentRoof = value;
                this.OnPropertyChanged();
            }
        }

        public SlopeWrapper SelectedSlope
        {
            get
            {
                return this._selectedSlope;
            }
            set
            {
                this._selectedSlope = value;
                this.OnPropertyChanged();
                ((DelegateCommand)RemoveCommand).RaiseCanExecuteChanged();
            }
        }

        public SlopeWrapper Slope
        {
            get { return _slope; }
            set
            {
                _slope = value;
                OnPropertyChanged();
            }
        }

        public ICommand NextCommand { get; }

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
            var slope = new Slope
                            {
                                SlopeType = Rectangle,
                                ModuleCost = 10.99M,
                                ModuleEffectiveSquare = 0.77,
                                SideA = 0,
                                SideB = 0,
                                SlopeHeight = 0
                            };

            var roof = new RoofWrapper(new Roof());
            CurrentRoof = roof;

            return slope;
        }

        private async Task LoadProductLookupAsync()
        {
            Roofing.Clear();
            var lookup = await _productLookupDataService.GetRoofingLookupAsync();
            foreach (var lookupItem in lookup)
            {
                Roofing.Add(lookupItem);
            }
        }

        private void OnNextExecute()
        {
            switch (Slope.SlopeType)
            {
                case Rectangle:
                    SelectedViewModel = new RectangularSlopeViewModel(Slope);
                    break;
                case Triangle:
                    SelectedViewModel = new TriangularSlopeViewModel(Slope);
                    break;
                case Trapeze:
                    SelectedViewModel = new TrapezoidalSlopeViewModel(Slope);
                    break;
                case Parallelogram:
                    SelectedViewModel = new ParallelogramSlopeViewModel(Slope);
                    break;
            }

            Slope.SideA = 0;
            Slope.SideB = 0;
            Slope.SlopeHeight = 0;
        }
        
        private void OnAddExecute()
        {
            SlopeCalculate();
            var wrapper = new SlopeWrapper(new Slope());
            wrapper.SlopeType = Slope.SlopeType;
            wrapper.SlopeSquare = Slope.SlopeSquare;
            wrapper.ModuleCost = Slope.ModuleCost;
            wrapper.ModuleQuantity = Slope.ModuleQuantity;
            wrapper.Summ = Slope.Summ;

            wrapper.PropertyChanged += Wrapper_PropertyChanged;
            Slopes.Add(wrapper);

            CurrentRoof.RoofSquare += Slope.SlopeSquare;
            CurrentRoof.TotalQuantity += Slope.ModuleQuantity;
            CurrentRoof.TotalSum += Slope.Summ;
        }

        private void Wrapper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SlopeWrapper.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        private void SlopeCalculate()
        {
            double quantity = 0;
            switch (Slope.SlopeType)
            {
                case Rectangle:
                    Slope.SlopeSquare = Slope.SideA * Slope.SideB;
                    break;
                case Triangle:
                    Slope.SlopeSquare = (Slope.SideA * Slope.SlopeHeight) / 2.0;
                    break;
                case Trapeze:
                    Slope.SlopeSquare = Slope.SlopeHeight * (Slope.SideA + Slope.SideB) / 2.0;
                    break;
                case Parallelogram:
                    Slope.SlopeSquare = Slope.SideA * Slope.SlopeHeight;
                    break;
            }

            quantity = (Slope.SlopeSquare / Slope.ModuleEffectiveSquare) * 1.02;
            Slope.ModuleQuantity = Convert.ToInt32(Math.Round(quantity));
            Slope.Summ = Slope.ModuleCost * Slope.ModuleQuantity;
        }

        private bool OnRemoveCanExecute()
        {
            return SelectedSlope != null;
        }

        private void OnRemoveExecute()
        {
            CurrentRoof.RoofSquare -= SelectedSlope.SlopeSquare;
            CurrentRoof.TotalQuantity -= SelectedSlope.ModuleQuantity;
            CurrentRoof.TotalSum -= SelectedSlope.Summ;
            Slopes.Remove(SelectedSlope);
            SelectedSlope = null;
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
