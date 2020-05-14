using SkiaSharp;

namespace FancyFrameApp.Extensions
{
    public  static class SkPathExtensions
    {
        private const int DEFAULT_XAXISROTATE = 0;

        public  static SKPath ArcTo(this SKPath path, float radius, SKPoint finalPoint)
        {
            path.ArcTo(
                new SKPoint(radius, radius),
                DEFAULT_XAXISROTATE,
                SKPathArcSize.Small, SKPathDirection.Clockwise,
                finalPoint);

            return path;
        }
    }
}
