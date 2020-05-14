using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace FancyFrameApp.Extensions
{
    internal static class ColorExtensions
    {
        private const float DEFAULT_SIGMA = -6f;
        public static SKImageFilter ToSKDropShadow(this Color shadowColor, float distance) =>
              SKImageFilter.CreateDropShadow(
                    distance,
                    distance,
                    DEFAULT_SIGMA,
                    DEFAULT_SIGMA,
                    shadowColor.ToSKColor(), //0.75 of opacity. Taken form UWP renderer
                    SKDropShadowImageFilterShadowMode.DrawShadowOnly);

    }
}
