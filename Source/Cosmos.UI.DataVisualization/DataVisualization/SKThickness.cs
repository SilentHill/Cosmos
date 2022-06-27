using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmos.Widgets.Primitives.Abstractions.DataVisualization
{

    public struct SKThickness : IEquatable<SKThickness>
    {
        public SKThickness(float uniformLength)
        {
            _Left = _Top = _Right = _Bottom = uniformLength;
        }
        public SKThickness(float left, float top, float right, float bottom)
        {
            _Left = left;
            _Top = top;
            _Right = right;
            _Bottom = bottom;
        }
        public override bool Equals(object obj)
        {
            if (obj is SKThickness)
            {
                SKThickness otherObj = (SKThickness)obj;
                return (this == otherObj);
            }
            return (false);
        }
        public bool Equals(SKThickness thickness)
        {
            return (this == thickness);
        }
        public override int GetHashCode()
        {
            return _Left.GetHashCode() ^ _Top.GetHashCode() ^ _Right.GetHashCode() ^ _Bottom.GetHashCode();
        }
        public override string ToString()
        {
            return String.Empty;
        }
        internal bool IsZero
        {
            get
            {
                return FloatUtil.IsZero(Left)
                        && FloatUtil.IsZero(Top)
                        && FloatUtil.IsZero(Right)
                        && FloatUtil.IsZero(Bottom);
            }
        }
        internal bool IsUniform
        {
            get
            {
                return FloatUtil.AreClose(Left, Top)
                        && FloatUtil.AreClose(Left, Right)
                        && FloatUtil.AreClose(Left, Bottom);
            }
        }
        internal bool IsValid(bool allowNegative, bool allowNaN, bool allowPositiveInfinity, bool allowNegativeInfinity)
        {
            if (!allowNegative)
            {
                if (Left < 0d || Right < 0d || Top < 0d || Bottom < 0d)
                {
                    return false;
                }
            }

            if (!allowNaN)
            {
                if (FloatUtil.IsNaN(Left) || FloatUtil.IsNaN(Right) || FloatUtil.IsNaN(Top) || FloatUtil.IsNaN(Bottom))
                {
                    return false;
                }
            }

            if (!allowPositiveInfinity)
            {
                if (Double.IsPositiveInfinity(Left) || Double.IsPositiveInfinity(Right) || Double.IsPositiveInfinity(Top) || Double.IsPositiveInfinity(Bottom))
                {
                    return false;
                }
            }

            if (!allowNegativeInfinity)
            {
                if (Double.IsNegativeInfinity(Left) || Double.IsNegativeInfinity(Right) || Double.IsNegativeInfinity(Top) || Double.IsNegativeInfinity(Bottom))
                {
                    return false;
                }
            }

            return true;
        }
        internal bool IsClose(SKThickness thickness)
        {
            return (FloatUtil.AreClose(Left, thickness.Left)
                    && FloatUtil.AreClose(Top, thickness.Top)
                    && FloatUtil.AreClose(Right, thickness.Right)
                    && FloatUtil.AreClose(Bottom, thickness.Bottom));
        }
        static internal bool AreClose(SKThickness thickness0, SKThickness thickness1)
        {
            return thickness0.IsClose(thickness1);
        }
        public static bool operator ==(SKThickness t1, SKThickness t2)
        {
            return ((t1._Left == t2._Left || (FloatUtil.IsNaN(t1._Left) && FloatUtil.IsNaN(t2._Left)))
                    && (t1._Top == t2._Top || (FloatUtil.IsNaN(t1._Top) && FloatUtil.IsNaN(t2._Top)))
                    && (t1._Right == t2._Right || (FloatUtil.IsNaN(t1._Right) && FloatUtil.IsNaN(t2._Right)))
                    && (t1._Bottom == t2._Bottom || (FloatUtil.IsNaN(t1._Bottom) && FloatUtil.IsNaN(t2._Bottom)))
                    );
        }
        public static bool operator !=(SKThickness t1, SKThickness t2)
        {
            return (!(t1 == t2));
        }
        public float Left
        {
            get { return _Left; }
            set { _Left = value; }
        }
        public float Top
        {
            get { return _Top; }
            set { _Top = value; }
        }
        public float Right
        {
            get { return _Right; }
            set { _Right = value; }
        }
        public float Bottom
        {
            get { return _Bottom; }
            set { _Bottom = value; }
        }
        internal SKSize Size
        {
            get
            {
                return new SKSize(_Left + _Right, _Top + _Bottom);
            }
        }
        private float _Left;
        private float _Top;
        private float _Right;
        private float _Bottom;
    }
    internal static class FloatUtil
    {
        internal static float FLT_EPSILON = 1.192092896e-07F;
        internal static float FLT_MAX_PRECISION = 0xffffff;
        internal static float INVERSE_FLT_MAX_PRECISION = 1.0F / FLT_MAX_PRECISION;
        public static bool AreClose(float a, float b)
        {
            if (a == b)
            {
                return true;
            }
            float eps = ((float)Math.Abs(a) + (float)Math.Abs(b) + 10.0f) * FLT_EPSILON;
            float delta = a - b;
            return (-eps < delta) && (eps > delta);
        }
        public static bool IsOne(float a)
        {
            return (float)Math.Abs(a - 1.0f) < 10.0f * FLT_EPSILON;
        }
        public static bool IsNaN(float a)
        {
            return float.IsNaN(a);
        }
        public static bool IsZero(float a)
        {
            return (float)Math.Abs(a) < 10.0f * FLT_EPSILON;
        }
        public static bool IsCloseToDivideByZero(float numerator, float denominator)
        {
            return Math.Abs(denominator) <= Math.Abs(numerator) * INVERSE_FLT_MAX_PRECISION;
        }

    }
}
