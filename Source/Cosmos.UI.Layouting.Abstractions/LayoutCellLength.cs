using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Cosmos.UI.Layoutting.Abstractions
{
    public enum LayoutCellUnitType
    {
        Auto = 0,
        Pixel,
        Star,
    }
    public struct LayoutCellLength : IEquatable<LayoutCellLength>
    {
        public LayoutCellLength(double pixels)
            : this(pixels, LayoutCellUnitType.Pixel)
        {
        }
        public LayoutCellLength(double value, LayoutCellUnitType type)
        {
            if (Double.IsNaN(value))
            {
                throw new ArgumentException("bad length");
            }
            if (double.IsInfinity(value))
            {
                throw new ArgumentException("bad length");
            }
            if (type != LayoutCellUnitType.Auto
                && type != LayoutCellUnitType.Pixel
                && type != LayoutCellUnitType.Star)
            {
                throw new ArgumentException("bad length");
            }

            _unitValue = (type == LayoutCellUnitType.Auto) ? 0.0 : value;
            _unitType = type;
        }
        public static bool operator ==(LayoutCellLength length1, LayoutCellLength length2)
        {
            return (length1.LayoutCellUnitType == length2.LayoutCellUnitType
                    && length1.Value == length2.Value);
        }
        public static bool operator !=(LayoutCellLength length1, LayoutCellLength length2)
        {
            return (length1.LayoutCellUnitType != length2.LayoutCellUnitType
                    || length1.Value != length2.Value);
        }
        override public bool Equals(object oCompare)
        {
            if (oCompare is LayoutCellLength)
            {
                LayoutCellLength l = (LayoutCellLength)oCompare;
                return (this == l);
            }
            else
                return false;
        }
        public bool Equals(LayoutCellLength layoutCellLength)
        {
            return (this == layoutCellLength);
        }
        public override int GetHashCode()
        {
            return ((int)_unitValue + (int)_unitType);
        }
        public bool IsAbsolute { get { return (_unitType == LayoutCellUnitType.Pixel); } }
        public bool IsAuto { get { return (_unitType == LayoutCellUnitType.Auto); } }
        public bool IsStar { get { return (_unitType == LayoutCellUnitType.Star); } }
        public double Value { get { return ((_unitType == LayoutCellUnitType.Auto) ? 1.0 : _unitValue); } }
        public LayoutCellUnitType LayoutCellUnitType { get { return (_unitType); } }
        public override string ToString()
        {
            switch (LayoutCellUnitType)
            {
                case (LayoutCellUnitType.Auto):
                    {
                        return ("Auto");
                    }
                case (LayoutCellUnitType.Star):
                    if (IsOne(Value))
                    {
                        return "*";
                    }
                    else
                    {
                        return Convert.ToString(Value) + "*";
                    }
                default:
                    return Convert.ToString(Value);
            }
        }
        internal const double DBL_EPSILON = 2.2204460492503131e-016; /* smallest such that 1.0+DBL_EPSILON != 1.0 */
        private static bool IsOne(double value)
        {
            return Math.Abs(value - 1.0) < 10.0 * DBL_EPSILON;
        }
        public static LayoutCellLength Auto
        {
            get { return (s_auto); }
        }
        private double _unitValue;
        private LayoutCellUnitType _unitType;

        private static readonly LayoutCellLength s_auto = new LayoutCellLength(1.0, LayoutCellUnitType.Auto);

        public static LayoutCellLength FromString(String s)
        {
            string goodString = s.Trim().ToLowerInvariant();

            int i;
            int strLen = goodString.Length;
            int strLenUnit = 0;
            double unitFactor = 1.0;

            i = 0;
            LayoutCellUnitType unit = LayoutCellUnitType.Auto;
            double value;
            if (goodString == UnitStrings[i])
            {
                strLenUnit = UnitStrings[i].Length;
                unit = (LayoutCellUnitType)i;
            }
            else
            {
                for (i = 1; i < UnitStrings.Length; ++i)
                {
                    if (goodString.EndsWith(UnitStrings[i], StringComparison.Ordinal))
                    {
                        strLenUnit = UnitStrings[i].Length;
                        unit = (LayoutCellUnitType)i;
                        break;
                    }
                }
            }

            if (i >= UnitStrings.Length)
            {
                for (i = 0; i < PixelUnitStrings.Length; ++i)
                {
                    if (goodString.EndsWith(PixelUnitStrings[i], StringComparison.Ordinal))
                    {
                        strLenUnit = PixelUnitStrings[i].Length;
                        unitFactor = PixelUnitFactors[i];
                        break;
                    }
                }
            }

            if (strLen == strLenUnit
                && (unit == LayoutCellUnitType.Auto
                    || unit == LayoutCellUnitType.Star))
            {
                value = 1;
            }
            else
            {
                string valueString = goodString.Substring(0, strLen - strLenUnit);
                value = Convert.ToDouble(valueString) * unitFactor;
            }

            var cell_length = new LayoutCellLength(value, unit);
            return cell_length;
        }
        static private string[] UnitStrings = { "auto", "px", "*" };

        static private string[] PixelUnitStrings = { "in", "cm", "pt" };
        static private double[] PixelUnitFactors =
        {
            96.0,
            96.0 / 2.54,
            96.0 / 72.0,
        };
        public static LayoutCellLength BadLength { get; } = new LayoutCellLength(0, LayoutCellUnitType.Pixel);
    }
}
