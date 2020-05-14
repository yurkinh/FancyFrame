using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace FancyFrameApp.Utils
{
    public static class ShapeUtils
    {
        public static SKPath CreateRoundedRectSKPath(float rectWidth, float rectHeight, float topLeft, float topRight, float bottomRight, float bottomLeft, float scale = 1)
        {
            var SKPath = new SKPath();
            var radii = new SKPoint[] {new SKPoint(topLeft *scale, topLeft*scale),
                                new SKPoint(topRight*scale, topRight*scale),
                                new SKPoint(bottomRight*scale, bottomRight*scale),
                                new SKPoint(bottomLeft*scale, bottomLeft*scale) };

            var skrect = new SKRect(0, 0, rectWidth, rectHeight);
            var skroundRect = new SKRoundRect();
            skroundRect.SetRectRadii(skrect, radii);
            SKPath.AddRoundRect(skroundRect, SKPathDirection.Clockwise);
            SKPath.Close();

            return SKPath;
        }

        public static SKPath CreatePolygonPath(double rectWidth, double rectHeight, int sides, double cornerRadius = 0.0, double rotationOffset = 0.0)
        {
            var offsetRadians = rotationOffset * Math.PI / 180;

            var SKPath = new SKPath();
            var theta = 2 * Math.PI / sides;

            // depends on the rotation
            var width = (-cornerRadius + Math.Min(rectWidth, rectHeight)) / 2;
            var center = new SKPoint((float)rectWidth / 2, (float)rectHeight / 2);

            var radius = width + cornerRadius - Math.Cos(theta) * cornerRadius / 2;

            var angle = offsetRadians;
            var corner = new SKPoint((float)(center.X + (radius - cornerRadius) * Math.Cos(angle)), (float)(center.Y + (radius - cornerRadius) * Math.Sin(angle)));
            SKPath.MoveTo((float)(corner.X + cornerRadius * Math.Cos(angle + theta)), (float)(corner.Y + cornerRadius * Math.Sin(angle + theta)));

            for (var i = 0; i < sides; i++)
            {
                angle += theta;
                corner = new SKPoint((float)(center.X + (radius - cornerRadius) * Math.Cos(angle)), (float)(center.Y + (radius - cornerRadius) * Math.Sin(angle)));
                var tip = new SKPoint((float)(center.X + radius * Math.Cos(angle)), (float)(center.Y + radius * Math.Sin(angle)));
                var start = new SKPoint((float)(corner.X + cornerRadius * Math.Cos(angle - theta)), (float)(corner.Y + cornerRadius * Math.Sin(angle - theta)));
                var end = new SKPoint((float)(corner.X + cornerRadius * Math.Cos(angle + theta)), (float)(corner.Y + cornerRadius * Math.Sin(angle + theta)));

                SKPath.LineTo(start.X, start.Y);
                SKPath.QuadTo(tip.X, tip.Y, end.X, end.Y);
            }

            SKPath.Close();

            return SKPath;
        }
    }
}
