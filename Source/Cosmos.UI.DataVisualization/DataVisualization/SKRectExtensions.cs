using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmos.Widgets.Primitives.Abstractions.DataVisualization
{
    public static class SKRectExtensions
    {
        public static SKPoint TopLeft(this SKRect skRect)
        {
            return new SKPoint(skRect.Left, skRect.Top);
        }
        public static SKPoint TopRight(this SKRect skRect)
        {
            return new SKPoint(skRect.Right, skRect.Top);
        }
        public static SKPoint BottomLeft(this SKRect skRect)
        {
            return new SKPoint(skRect.Left, skRect.Bottom);
        }
        public static SKPoint BottomRight(this SKRect skRect)
        {
            return new SKPoint(skRect.Right, skRect.Bottom);
        }
    }
}
