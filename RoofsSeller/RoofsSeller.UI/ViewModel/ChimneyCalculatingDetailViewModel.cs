using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using RoofsSeller.Model;
using RoofsSeller.Model.Entities;
using RoofsSeller.UI.Data.Lookups;
using RoofsSeller.UI.Data.Repositories;
using RoofsSeller.UI.View.Services;
using RoofsSeller.UI.ViewModel.ShellTypesViewModel;
using RoofsSeller.UI.ViewModel.UmbrellaTypesViewModel;
using RoofsSeller.UI.Wrapper;

namespace RoofsSeller.UI.ViewModel
{
    using RoofsSeller.UI.ViewModel.Abutment;

    internal sealed class ChimneyCalculatingDetailViewModel : DetailViewModelBase
    {
        private readonly IProductLookupDataService _productLookupDataService;
        private PipeAbutmentWrapper _pipeAbutment;
        private PipeWrapper _pipe;
        private PipeShellWrapper _pipeShell;
        private PipeUmbrellaWrapper _pipeUmbrella;

        public ChimneyCalculatingDetailViewModel(IEventAggregator eventAggregator, 
            IMessageDialogService messageDialogService,
            IProductRepository productRepository,
            IProductLookupDataService productLookupDataService
            ) : base(eventAggregator, messageDialogService)
        {
            _productLookupDataService = productLookupDataService;

            Title = "Калькулятор вентканала";

            Metals = new ObservableCollection<LookupItem>();

            KindsOfShell = new ObservableCollection<string> {"Вертикальная", "Горизонтальная", "Штучная"};
            KindsOfUmbrella = new ObservableCollection<string> {"Эконом", "Стандарт", "Премиум"};

            CalculateCommand = new DelegateCommand(OnCalculateExecute, OnCalculateCanExecute);
        }

        public ObservableCollection<LookupItem> Metals { get; }

        public ObservableCollection<string> KindsOfShell { get; }

        public ObservableCollection<string> KindsOfUmbrella { get; }

        public PipeAbutmentWrapper PipeAbutment
        {
            get { return _pipeAbutment; }
            set
            {
                _pipeAbutment = value;
                OnPropertyChanged();
            }
        }

        public PipeWrapper Pipe
        {
            get { return _pipe; }
            set
            {
                _pipe = value;
                OnPropertyChanged();
                ((DelegateCommand)CalculateCommand).RaiseCanExecuteChanged();
            }
        }

        public PipeShellWrapper PipeShell
        {
            get { return _pipeShell; }
            set
            {
                _pipeShell = value;
                OnPropertyChanged();
            }
        }

        public PipeUmbrellaWrapper PipeUmbrella
        {
            get { return _pipeUmbrella; }
            set
            {
                _pipeUmbrella = value;
                OnPropertyChanged();
            }
        }

        public ICommand CalculateCommand { get; }

        public override async Task LoadAsync(int id)
        {
            Id = id;

            var pipe = CreateNewPipe();

            var abutment = new PipeAbutment();
            PipeAbutment = new PipeAbutmentWrapper(abutment);
            var shell = new PipeShell
            {
                KindOfShell = "Вертикальная"
            };
            PipeShell = new PipeShellWrapper(shell);

            var umbrella = new PipeUmbrella
            {
                KindOfUmbrella = "Эконом"
            };

            PipeUmbrella = new PipeUmbrellaWrapper(umbrella);

            InitializePipe(pipe);

            await LoadProductLookupAsync();
        }

        private Pipe CreateNewPipe()
        {
            var pipe = new Pipe
            {
                InsulationThickness = 50,
                Abutment = new PipeAbutment(),
                Shell = new PipeShell(),
                Umbrella = new PipeUmbrella()
            };
            return pipe;
        }

        private void InitializePipe(Pipe pipe)
        {
            Pipe = new PipeWrapper(pipe);
            
        }

        private async Task LoadProductLookupAsync()
        {
            Metals.Clear();
            //Metals.Add(new NullLookupItem { DisplayMember = " - " });
            var lookup = await _productLookupDataService.GetMetalLookupAsync();
            foreach (var lookupItem in lookup)
            {
                Metals.Add(lookupItem);
            }
        }

        private bool OnCalculateCanExecute()
        {
            return Pipe != null
                   && !Pipe.HasErrors;
        }

        private void OnCalculateExecute()
        {
            EnterData();
        }

        #region InputData Members

        private async void EnterData()
        {
            var a = Pipe.PipeWidth + Pipe.InsulationThickness * 2 + Pipe.BarThickness * 2 + Pipe.ContrBarThickness * 2;
            var b = Pipe.PipeLength + Pipe.InsulationThickness * 2 + Pipe.BarThickness * 2 + Pipe.ContrBarThickness * 2;

            double d = 0;

            // Если пользователь ввел угол кровли и одну из высот (большую)
            if (Pipe.RoofAngle != 0 && (Pipe.PipeHeight1 != 0 || Pipe.PipeHeight2 != 0) && (Pipe.PipeHeight1 == 0 || Pipe.PipeHeight2 == 0))
            {
                await CheckRoofAngle();

                d = b / Math.Cos(Pipe.RoofAngle * Math.PI / 180); // расчет гипотенузы d, угол кровли предвариательно переводиться в радианы

                var katet = Convert.ToInt32(Math.Sqrt(Math.Pow(d, 2) - Math.Pow(b, 2)));

                if (Pipe.PipeHeight1 < Pipe.PipeHeight2)
                    SwapPipeHeights();

                Pipe.PipeHeight2 = Pipe.PipeHeight1 - katet;

                await CheckPipeHeight2();
            }
            // Если пользователь ввел высоты трубы h1 и h2
            else if (Pipe.RoofAngle == 0 && Pipe.PipeHeight1 != 0 && Pipe.PipeHeight2 != 0)
            {
                if (Pipe.PipeHeight1 < Pipe.PipeHeight2)
                    SwapPipeHeights();

                d = Math.Sqrt(Math.Pow(b, 2) + Math.Pow((Math.Abs(Pipe.PipeHeight1 - Pipe.PipeHeight2)), 2));

                var angleInRadians = Math.Acos(b / d);
                var angleInDegrees = angleInRadians * 180 / Math.PI;

                Pipe.RoofAngle = Convert.ToInt32(Math.Round(angleInDegrees));

                await CheckRoofAngle();
            }
            // Если пользователь ввел и угол кровли и высоты h1, h2
            else if (Pipe.RoofAngle != 0 && Pipe.PipeHeight1 != 0 && Pipe.PipeHeight2 != 0)
            {
                d = b / Math.Cos(Pipe.RoofAngle * Math.PI / 180); // расчет гипотенузы d, угол кровли предвариательно переводиться в радианы

                var katet = Convert.ToInt32(Math.Sqrt(Math.Pow(d, 2) - Math.Pow(b, 2)));

                if (Pipe.PipeHeight1 < Pipe.PipeHeight2)
                    SwapPipeHeights();

                Pipe.PipeHeight2 = Pipe.PipeHeight1 - katet;

                await CheckPipeHeight2();
            }

            AbutmentCalculate(d, a);
            ShellCalculate(a, b);
            UmbrellaCalculate(a, b);
        }

        private async Task CheckPipeHeight2()
        {
            if (Pipe.PipeHeight2 <= 0)
            {
                await MessageDialogService.ShowInfoDialogAsync(
                    $"Введена недопустимая высота трубы h1. При угле кровли {Pipe.RoofAngle} град., высота трубы h1 должна быть больше. " +
                    $"Уточните значение h1 и нажмите кнопку \"Рассчитать\"");
                Pipe.PipeHeight2 = 0;
                // Do smthg with PipeHeight1 and re-count PipeHeight2
            }
        }

        private async Task CheckRoofAngle()
        {
            if (Pipe.RoofAngle < 15 || Pipe.RoofAngle > 50)
            {
                await MessageDialogService.ShowInfoDialogAsync(
                    $"Введен недопустимый угол кровли. Значение угла кровли должно быть в пределах 15-80 град. " +
                    $"Уточните значение угла кровли и нажмите кнопку \"Рассчитать\"");
                Pipe.RoofAngle = 15;
                Pipe.PipeHeight2 = 0;
            }
        }

        private void SwapPipeHeights()
        {
            int temp = Pipe.PipeHeight1;
            Pipe.PipeHeight1 = Pipe.PipeHeight2;
            Pipe.PipeHeight2 = temp;
        }

        #endregion

        #region PipeAbutment Members

        private object _selectedAbutmentViewModel;

        public object SelectedAbutmentViewModel
        {
            get { return _selectedAbutmentViewModel; }
            set
            {
                _selectedAbutmentViewModel = value;
                OnPropertyChanged();
            }
        }

        private void AbutmentCalculate(double d, int a)
        {
            SelectedAbutmentViewModel = new AbutmentViewModel(PipeAbutment);
            SidePartSizeCalculate(d);
            FrontPartSizeCalculate(a);
            BackPartSizeCalculate(a);
            CostCalculate();
        }

        private void BackPartSizeCalculate(int a)
        {
            PipeAbutment.BackWidth = 450;
            PipeAbutment.BackLength = a + 45;
        }

        private void FrontPartSizeCalculate(int a)
        {
            if (Pipe.RoofAngle <= 35)
            {
                PipeAbutment.FrontWidth = 450;
            }
            else if (Pipe.RoofAngle > 35 && Pipe.RoofAngle <= 45)
            {
                PipeAbutment.FrontWidth = 500;
            }
            else if (Pipe.RoofAngle > 45)
            {
                PipeAbutment.FrontWidth = 550;
            }

            PipeAbutment.FrontLength = a + 21;
        }

        private void SidePartSizeCalculate(double di)
        {
            PipeAbutment.SideWidth = 400;
            var d = Convert.ToInt32(di);

            if (Pipe.RoofAngle <= 25)
            {
                PipeAbutment.SideLength = d + 650;
            }
            else if (Pipe.RoofAngle > 25 && Pipe.RoofAngle <= 35)
            {
                PipeAbutment.SideLength = d + 700;
            }
            else if (Pipe.RoofAngle > 35 && Pipe.RoofAngle <= 45)
            {
                PipeAbutment.SideLength = d + 750;
            }
            else if (Pipe.RoofAngle > 45)
            {
                PipeAbutment.SideLength = d + 800;
            }
        }

        private void CostCalculate()
        {
            var squareSum = (PipeAbutment.SideLength * PipeAbutment.SideWidth * 2 +
                            PipeAbutment.FrontLength * PipeAbutment.FrontWidth +
                            PipeAbutment.BackLength * PipeAbutment.BackWidth) / 1000000.0;

            if (squareSum <= 2.2)
            {
                PipeAbutment.MetalSheetQuantityRequired = 1;
                PipeAbutment.AbutmentCost = Pipe.MetalSheetCost * PipeAbutment.MetalSheetQuantityRequired + 95.5M;
            }
            else if (squareSum > 2.2)
            {
                PipeAbutment.MetalSheetQuantityRequired = 2;
                PipeAbutment.AbutmentCost = Pipe.MetalSheetCost * PipeAbutment.MetalSheetQuantityRequired + 95.5M;
            }
        }

        #endregion

        #region PipeShell Members

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

        private void ShellCalculate(int a, int b)
        {
            switch (PipeShell.KindOfShell)
            {
                case "Вертикальная":
                    SelectedViewModel = new VerticalShellViewModel(Pipe, PipeShell);
                    VerticalShellCalculate(a, b);
                    break;
                case "Горизонтальная":
                    SelectedViewModel = new HorizontalShellViewModel(Pipe, PipeShell);
                    HorizontalShellCalculate(a, b);
                    break;
                case "Штучная":
                    SelectedViewModel = new ExclusiveShellViewModel(Pipe, PipeShell);
                    ExclusiveShellCalculate(a, b);
                    break;
            }
        }

        private async void VerticalShellCalculate(int a, int b)
        {
            var tupleA = GetNamedValues(a);
            var tupleB = GetNamedValues(b);

            PipeShell.MainBarWidthSide13 = tupleA.mainBarWidth;
            PipeShell.MainBarQuantitySide13 = tupleA.mainBarQuantity;
            PipeShell.MainBarWidthSide24 = tupleB.mainBarWidth;
            PipeShell.MainBarQuantitySide24 = tupleB.mainBarQuantity;

            PipeShell.InnerBarWidthSide13 = 80;
            PipeShell.InnerBarQuantitySide13 = tupleA.innerBarQuantity;
            PipeShell.InnerBarWidthSide24 = 80;
            PipeShell.InnerBarQuantitySide24 = tupleB.innerBarQuantity;
            PipeShell.CornerBarWidth = 90;

            int h = 0;

            if (Pipe.PipeHeight1 <= 1100)
            {
                h = 1000;
            }
            else if (Pipe.PipeHeight1 > 1100 && Pipe.PipeHeight1 <= 1350)
            {
                h = 1250;
            }
            else if (Pipe.PipeHeight1 > 1350 && Pipe.PipeHeight1 <= 2100)
            {
                h = 2000;
            }
            else if (Pipe.PipeHeight1 > 2100 && Pipe.PipeHeight1 <= 2600)
            {
                h = 2500;
            }
            else if (Pipe.PipeHeight1 > 2600)
            {
                await MessageDialogService.ShowInfoDialogAsync(
                    $"Недопустимо использовать вертикальную обшивку при высоте трубы более 2600 мм." +
                    $"Используйте другой вид обшивки или уточните значение h1 и нажмите кнопку \"Рассчитать\"");
            }

            PipeShell.MainBarHeightSide13 = h;
            PipeShell.MainBarHeightSide24 = h;
            PipeShell.InnerBarHeightSide13 = h;
            PipeShell.InnerBarHeightSide24 = h;
            PipeShell.CornerBarHeight = h;

            PipeShell.MainBarMetalRequired = (PipeShell.MainBarHeightSide13 / 1000.0) *
                                             (PipeShell.MainBarWidthSide13 / 1000.0) *
                                             PipeShell.MainBarQuantitySide13 +
                                             (PipeShell.MainBarHeightSide24 / 1000.0) *
                                             (PipeShell.MainBarWidthSide24 / 1000.0) *
                                             PipeShell.MainBarQuantitySide24;

            PipeShell.InnerCornerSubBarMetalRequired = (PipeShell.InnerBarHeightSide13 / 1000.0) *
                                                       (PipeShell.InnerBarWidthSide13 / 1000.0) *
                                                       PipeShell.InnerBarQuantitySide13 +
                                                       (PipeShell.InnerBarHeightSide24 / 1000.0) *
                                                       (PipeShell.InnerBarWidthSide24 / 1000.0) *
                                                       PipeShell.InnerBarQuantitySide24 +
                                                       (PipeShell.CornerBarHeight / 1000.0) *
                                                       (PipeShell.CornerBarWidth / 1000.0) * 4;

            var metalRequired = PipeShell.MainBarMetalRequired + PipeShell.InnerCornerSubBarMetalRequired;

            if (metalRequired <= 1.9)
            {
                PipeShell.MetalRequired = 2.5;
            }
            else if (metalRequired > 1.9 && metalRequired <= 3.5)
            {
                PipeShell.MetalRequired = 5.0;
            }
            else if (metalRequired > 3.5 && metalRequired <= 6.0)
            {
                PipeShell.MetalRequired = 7.5;
            }
            else if (metalRequired > 6.0 && metalRequired <= 8.5)
            {
                PipeShell.MetalRequired = 10.0;
            }
            else if (metalRequired > 8.5 && metalRequired <= 11.0)
            {
                PipeShell.MetalRequired = 12.5;
            }
            else if (metalRequired > 11.0 && metalRequired <= 13.5)
            {
                PipeShell.MetalRequired = 15.0;
            }
            else if (metalRequired > 13.5 && metalRequired <= 16.0)
            {
                PipeShell.MetalRequired = 17.5;
            }

            PipeShell.ShellCost = Convert.ToDecimal(PipeShell.MetalRequired) * (6.25M + Pipe.MetalSheetCost / 2.5M);
        }

        // Returns tuple of named values
        private (int mainBarQuantity, int innerBarQuantity, int mainBarWidth, int metalRequired) GetNamedValues(int value)
        {
            var result = (mainBarQuantity:0, innerBarQuantity:0, mainBarWidth:0 , metalRequired:0);
            if (value <= 250)
            {
                var width = value + 10;
                result = (2, 0, width, 360);
            }
            else if (value > 250 && value <= 510)
            {
                var width = Convert.ToInt32(Math.Round((value - 50) / 2.0 + 40));
                result = (4, 2, width, 520);
            }
            else if (value > 510 && value <= 760)
            {
                var width = Convert.ToInt32(Math.Round((value - 70) / 3.0 + 40));
                result = (6, 4, width, 680);
            }
            else if (value > 760 && value <= 1010)
            {
                var width = Convert.ToInt32(Math.Round((value - 90) / 4.0 + 40));
                result = (8, 6, width, 840);
            }
            else if (value > 1010 && value <= 1260)
            {
                var width = Convert.ToInt32(Math.Round((value - 110) / 5.0 + 40));
                result = (10, 8, width, 1000);
            }
            else if (value > 1260 && value <= 1510)
            {
                var width = Convert.ToInt32(Math.Round((value - 130) / 6.0 + 40));
                result = (12, 10, width, 1160);
            }
            else if (value > 1510 && value <= 1760)
            {
                var width = Convert.ToInt32(Math.Round((value - 150) / 7.0 + 40));
                result = (14, 12, width, 1320);
            }
            else if (value > 1760 && value <= 2010)
            {
                var width = Convert.ToInt32(Math.Round((value - 170) / 8.0 + 40));
                result = (16, 14, width, 1480);
            }
            else if (value > 2010 && value <= 2260)
            {
                var width = Convert.ToInt32(Math.Round((value - 190) / 9.0 + 40));
                result = (18, 16, width, 1640);
            }
            return result;
        }

        private void HorizontalShellCalculate(int a, int b)
        {
            PipeShell.MainBarWidthSide13 = a;
            PipeShell.MainBarHeightSide13 = 250;
            PipeShell.MainBarWidthSide24 = b;
            PipeShell.MainBarHeightSide24 = 250;
            var quantity = Convert.ToInt32(Math.Round(Convert.ToDouble(2 * Pipe.PipeHeight1 / 190)));
            PipeShell.MainBarQuantitySide13 = quantity;
            PipeShell.MainBarQuantitySide24 = quantity;

            var mainBarSquareSide13 = RectangleSquareCalc(a, 250);
            var mainBarSquareSide24 = RectangleSquareCalc(b, 250);

            PipeShell.MainBarMetalRequired = quantity * (mainBarSquareSide13 + mainBarSquareSide24);

            PipeShell.InnerBarWidthSide13 = 58;
            PipeShell.InnerBarHeightSide13 = Pipe.PipeHeight1;

            var innerBarSquare = RectangleSquareCalc(58, PipeShell.InnerBarHeightSide13);

            PipeShell.CornerBarWidth = 140;
            PipeShell.CornerBarHeight = Pipe.PipeHeight1;

            var cornerBarSquare = RectangleSquareCalc(140, PipeShell.CornerBarHeight);

            PipeShell.SubBarHeightSide13 = 100;
            PipeShell.SubBarWidthSide13 = a;
            PipeShell.SubBarHeightSide24 = 100;
            PipeShell.SubBarWidthSide24 = a;

            var subBarSquareSide13 = RectangleSquareCalc(100, a);

            PipeShell.InnerCornerSubBarMetalRequired =
                innerBarSquare * 8 + cornerBarSquare * 8 + subBarSquareSide13 * 2 +
                subBarSquareSide13 * 2;

            var metalRequired = PipeShell.MainBarMetalRequired + PipeShell.InnerCornerSubBarMetalRequired;

            if (metalRequired <= 1.9)
            {
                PipeShell.MetalRequired = 2.5;
            }
            else if (metalRequired > 1.9 && metalRequired <= 3.5)
            {
                PipeShell.MetalRequired = 5.0;
            }
            else if (metalRequired > 3.5 && metalRequired <= 6.0)
            {
                PipeShell.MetalRequired = 7.5;
            }
            else if (metalRequired > 6.0 && metalRequired <= 8.5)
            {
                PipeShell.MetalRequired = 10.0;
            }
            else if (metalRequired > 8.5 && metalRequired <= 11.0)
            {
                PipeShell.MetalRequired = 12.5;
            }
            else if (metalRequired > 11.0 && metalRequired <= 13.5)
            {
                PipeShell.MetalRequired = 15.0;
            }

            PipeShell.ShellCost = Convert.ToDecimal(PipeShell.MetalRequired) * (Pipe.MetalSheetCost / 2.5M + 8M);
        }

        private double RectangleSquareCalc(int value1, int value2)
        {
            return value1 / 1000.0 * value2 / 1000.0;
        }

        private void ExclusiveShellCalculate(int a, int b)
        {
            var cost = Convert.ToDouble(30M + 1.5M * Pipe.MetalSheetCost / 2.5M);
            
            PipeShell.ShellCost =
                Convert.ToDecimal(Pipe.PipeHeight1 / 1000.0 * 2 * (a / 1000.0 + b / 1000.0) * cost);
        }

        #endregion

        #region PipeUmbrella Members

        private object _selectedUmbrellaViewModel;

        public object SelectedUmbrellaViewModel
        {
            get { return _selectedUmbrellaViewModel; }
            set
            {
                _selectedUmbrellaViewModel = value;
                OnPropertyChanged();
            }
        }

        private void UmbrellaCalculate(int a, int b)
        {
            int a1 = 0;
            int b1 = 0;
            if (PipeShell.KindOfShell is "Вертикальная")
            {
                a1 = a + 30;
                b1 = b + 30;
            }
            else
            {
                a1 = a + 50;
                b1 = b + 50;
            }

            switch (PipeUmbrella.KindOfUmbrella)
            {
                case "Эконом":
                    SelectedUmbrellaViewModel = new EconomUmbrellaViewModel(Pipe, PipeUmbrella);
                    EconomUmbrellaCalculate(a1, b1);
                    break;
                case "Стандарт":
                    SelectedUmbrellaViewModel = new StandartUmbrellaViewModel(Pipe, PipeUmbrella);
                    StandartUmbrellaCalculate(a1, b1);
                    break;
                case "Премиум":
                    SelectedUmbrellaViewModel = new PremiumUmbrellaViewModel(Pipe, PipeUmbrella);
                    PremiumUmbrellaCalculate(a1, b1);
                    break;
            }
        }

        private void EconomUmbrellaCalculate(int a1, int b1)
        {
            PipeUmbrella.FitWidthSide13 = 156;
            PipeUmbrella.FitLengthSide13 = a1 + 90;
            PipeUmbrella.FitQuantitySide13 = 2;
            PipeUmbrella.FitWidthSide24 = 156;
            PipeUmbrella.FitLengthSide24 = b1 + 90;
            PipeUmbrella.FitQuantitySide24 = 2;

            PipeUmbrella.LegWidth = 270;
            PipeUmbrella.LegHeight = 340;
            if (a1 <= 1000)
            {
                PipeUmbrella.LegQuantity = 4;
            }
            else if (a1 > 1000 && a1 <= 1800)
            {
                PipeUmbrella.LegQuantity = 6;
            }
            else if (a1 > 1800)
            {
                PipeUmbrella.LegQuantity = 8;
            }

            PipeUmbrella.SubBarHeight = 138;
            PipeUmbrella.SubBarWidth = b1 + 235;
            PipeUmbrella.SubBarQuantity = PipeUmbrella.LegQuantity / 2;

            PipeUmbrella.BottomPartWidth = a1 + 148;
            PipeUmbrella.BottomPartLength = b1 + 148;

            PipeUmbrella.ValmaWidth = Convert.ToInt32(Math.Round(((b1 + 115) / 0.9)));
            PipeUmbrella.ValmaLength = a1 + 115;

            PipeUmbrella.TriangleWidth = b1 + 130;
            PipeUmbrella.TriangleLength = Convert.ToInt32(Math.Round(((b1 + 115) / 0.9) / 2));

            UmbrellaMetalRequiredCalculate();

            UmbrellaCostCalculate();
        }

        private void StandartUmbrellaCalculate(int a1, int b1)
        {
            EconomUmbrellaCalculate(a1, b1);
            PipeUmbrella.LouversLengthSide13 = a1 + 7;
            PipeUmbrella.LouversWidthSide13 = 96;
            PipeUmbrella.LouversLengthSide24 = b1 + 7;
            PipeUmbrella.LouversWidthSide24 = 96;

            UmbrellaMetalRequiredCalculate();

            UmbrellaCostCalculate();
        }

        private void PremiumUmbrellaCalculate(int a1, int b1)
        {
            EconomUmbrellaCalculate(a1, b1);
            PipeUmbrella.LouversLengthSide13 = a1 + 25;
            PipeUmbrella.LouversWidthSide13 = 115;
            PipeUmbrella.LouversLengthSide24 = b1 + 25;
            PipeUmbrella.LouversWidthSide24 = 115;

            UmbrellaMetalRequiredCalculate();

            UmbrellaCostCalculate();
        }

        private void UmbrellaMetalRequiredCalculate()
        {
            double metalRequired = 0;
            if (PipeUmbrella.KindOfUmbrella == "Эконом")
            {
                metalRequired =
                (PipeUmbrella.FitLengthSide13 * PipeUmbrella.FitWidthSide13 * PipeUmbrella.FitQuantitySide13 +
                 PipeUmbrella.FitLengthSide24 * PipeUmbrella.FitWidthSide24 * PipeUmbrella.FitQuantitySide24 +
                 PipeUmbrella.LegHeight * PipeUmbrella.LegWidth * PipeUmbrella.LegQuantity +
                 PipeUmbrella.SubBarHeight * PipeUmbrella.SubBarWidth * PipeUmbrella.SubBarQuantity +
                 PipeUmbrella.BottomPartLength * PipeUmbrella.BottomPartWidth +
                 PipeUmbrella.ValmaLength * PipeUmbrella.ValmaWidth +
                 PipeUmbrella.TriangleLength * PipeUmbrella.TriangleWidth * 2) / 1000000.0;
            }
            else
            {
                metalRequired =
                (PipeUmbrella.FitLengthSide13 * PipeUmbrella.FitWidthSide13 * PipeUmbrella.FitQuantitySide13 +
                 PipeUmbrella.FitLengthSide24 * PipeUmbrella.FitWidthSide24 * PipeUmbrella.FitQuantitySide24 +
                 PipeUmbrella.LegHeight * PipeUmbrella.LegWidth * PipeUmbrella.LegQuantity +
                 PipeUmbrella.SubBarHeight * PipeUmbrella.SubBarWidth * PipeUmbrella.SubBarQuantity +
                 PipeUmbrella.BottomPartLength * PipeUmbrella.BottomPartWidth +
                 PipeUmbrella.ValmaLength * PipeUmbrella.ValmaWidth +
                 PipeUmbrella.TriangleLength * PipeUmbrella.TriangleWidth * 2 +
                 PipeUmbrella.LouversLengthSide13 * PipeUmbrella.LouversWidthSide13 * 6 +
                 PipeUmbrella.LouversLengthSide24 * PipeUmbrella.LouversWidthSide24 * 6) / 1000000.0;
            }

            if (metalRequired <= 1.9)
            {
                PipeUmbrella.MetalRequired = 2.5;
            }
            else if (metalRequired > 1.9 && metalRequired <= 3.5)
            {
                PipeUmbrella.MetalRequired = 5.0;
            }
            else if (metalRequired > 3.5 && metalRequired <= 6.0)
            {
                PipeUmbrella.MetalRequired = 7.5;
            }
            else if (metalRequired > 6.0 && metalRequired <= 8.5)
            {
                PipeUmbrella.MetalRequired = 10.0;
            }
            else if (metalRequired > 8.5 && metalRequired <= 11.0)
            {
                PipeUmbrella.MetalRequired = 12.5;
            }
            else if (metalRequired > 11.0 && metalRequired <= 13.5)
            {
                PipeUmbrella.MetalRequired = 15.0;
            }
            else if (metalRequired > 13.5 && metalRequired <= 15)
            {
                PipeUmbrella.MetalRequired = 17.5;
            }
            else if (metalRequired > 15.0 && metalRequired <= 18.5)
            {
                PipeUmbrella.MetalRequired = 20.0;
            }
            else if (metalRequired > 18.5 && metalRequired <= 21)
            {
                PipeUmbrella.MetalRequired = 22.5;
            }
        }

        private void UmbrellaCostCalculate()
        {
            if (PipeUmbrella.KindOfUmbrella == "Эконом")
            {
                PipeUmbrella.UmbrellaCost =
                    Pipe.MetalSheetCost / 2.5M * Convert.ToDecimal(PipeUmbrella.MetalRequired) + 40M;
            }
            else if (PipeUmbrella.KindOfUmbrella == "Стандарт")
            {
                PipeUmbrella.UmbrellaCost =
                    Pipe.MetalSheetCost / 2.5M * Convert.ToDecimal(PipeUmbrella.MetalRequired) + 50M;
            }
            else if (PipeUmbrella.KindOfUmbrella == "Премиум")
            {
                PipeUmbrella.UmbrellaCost =
                    Pipe.MetalSheetCost / 2.5M * Convert.ToDecimal(PipeUmbrella.MetalRequired) + 85M;
            }
        }

        #endregion

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
