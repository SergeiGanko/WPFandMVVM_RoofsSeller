using System;
using System.Collections.Generic;
using RoofsSeller.Model.Entities;

namespace RoofsSeller.UI.Wrapper
{
    public class PipeWrapper : ModelWrapper<Pipe>
    {
        public PipeWrapper(Pipe model) : base(model)
        {
        }

        public int Id { get { return Model.Id; } }

        public int PipeLength
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int PipeWidth
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int RoofAngle
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int PipeHeight1
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int PipeHeight2
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int InsulationThickness
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int BarThickness
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int ContrBarThickness
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public decimal MetalSheetCost
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }

        public decimal PipeCost
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }

        public PipeAbutment Abutment
        {
            get { return GetValue<PipeAbutment>(); }
            set { SetValue(value); }
        }

        public PipeShell Shell
        {
            get { return GetValue<PipeShell>(); }
            set { SetValue(value); }
        }

        public PipeUmbrella Umbrella
        {
            get { return GetValue<PipeUmbrella>(); }
            set { SetValue(value); }
        }

        #region Validation Property Members

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(PipeLength):
                    if (PipeLength > 10000)
                    {
                        yield return "Максимальная длина трубы 10000 мм";
                    }
                    else if (PipeLength < 0)
                    {
                        yield return "Значение длины трубы не может быть отрицательным";
                    }
                    break;
                case nameof(PipeWidth):
                    if (PipeWidth > 2260)
                    {
                        yield return "Максимальная ширина трубы 2260 мм";
                    }
                    else if (PipeWidth < 0)
                    {
                        yield return "Значение длины трубы не может быть отрицательным";
                    }
                    break;
                case nameof(RoofAngle):
                    if (RoofAngle > 90)
                    {
                        yield return "Неверное значение угла кровли";
                    }
                    else if (RoofAngle < 0)
                    {
                        yield return "Значение угла кровли не может быть отрицательным";
                    }
                    break;
                case nameof(PipeHeight1):
                    if (PipeHeight1 > 3000)
                    {
                        yield return "Значение высоты трубы слишком большое";
                    }
                    else if (PipeHeight1 < 0)
                    {
                        yield return "Значение не может быть отрицательным";
                    }
                    break;
                case nameof(PipeHeight2):
                    if (PipeHeight2 > 3000)
                    {
                        yield return "Значение высоты трубы слишком большое";
                    }
                    else if (PipeHeight2 < 0)
                    {
                        yield return "Значение не может быть отрицательным";
                    }
                    break;
                case nameof(BarThickness):
                    if (BarThickness > 100)
                    {
                        yield return "Значение толщины обрешетки слишком большое";
                    }
                    else if (BarThickness < 0)
                    {
                        yield return "Значение не может быть отрицательным";
                    }
                    break;
                case nameof(ContrBarThickness):
                    if (ContrBarThickness > 100)
                    {
                        yield return "Значение толщины котробрешетки слишком большое";
                    }
                    else if (ContrBarThickness < 0)
                    {
                        yield return "Значение не может быть отрицательным";
                    }
                    break;
                case nameof(InsulationThickness):
                    if (InsulationThickness > 300)
                    {
                        yield return "Значение толщины утеплителя слишком большое";
                    }
                    else if (InsulationThickness < 0)
                    {
                        yield return "Значение не может быть отрицательным";
                    }
                    break;
            }
        }

        #endregion
    }
}
