using FancyFrameApp.Extensions;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Linq;
using Xamarin.Forms;

namespace FancyFrameApp.Control
{
    public class FancyFrame : ContentView, IDisposable
    {
        private readonly Grid rootGrid;
        private readonly SKCanvasView canvas;
        private readonly float scale;
        private SKBitmap bitmap;
        public FancyFrame()
        {
            scale = Device.info.ScalingFactor == 0 ? 1 : (float)Device.info.ScalingFactor;

            rootGrid = new Grid()
            {
                Margin = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            canvas = new SKCanvasView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            canvas.PaintSurface += OnPaintSurface;
            rootGrid.Children.Add(canvas);

            base.Content = rootGrid;
        }

        #region Properties

        new public static readonly BindableProperty ContentProperty = BindableProperty.Create(nameof(Content), typeof(View), typeof(FancyFrame), null, propertyChanged: ContentPropertyChanged());
        new public static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(FancyFrame), Color.White);
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(FancyFrame), Color.Black);
        public static readonly BindableProperty BorderThicknessProperty = BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(FancyFrame), 0);
        public static readonly BindableProperty BorderIsDashedProperty = BindableProperty.Create(nameof(BorderIsDashed), typeof(bool), typeof(FancyFrame), default(bool));
        public static readonly BindableProperty BorderDrawingStyleProperty = BindableProperty.Create(nameof(BorderDrawingStyle), typeof(BorderDrawingStyle), typeof(FancyFrame), defaultValue: BorderDrawingStyle.Inside);

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(FancyFrame), default(CornerRadius));
        public static readonly BindableProperty HasShadowProperty = BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(FancyFrame), default(bool));
        public static readonly BindableProperty ElevationProperty = BindableProperty.Create(nameof(Elevation), typeof(int), typeof(FancyFrame), GetElavation());
        public static readonly BindableProperty ShadowDistanceProperty = BindableProperty.Create(nameof(ShadowDistance), typeof(double), typeof(FancyFrame), GetShadowDistance());        
        public static readonly BindableProperty LightShadowColorProperty = BindableProperty.Create(nameof(LightShadowColor), typeof(Color), typeof(FancyFrame), Color.LightGray);
        public static readonly BindableProperty DarkShadowColorProperty = BindableProperty.Create(nameof(DarkShadowColor), typeof(Color), typeof(FancyFrame), Color.LightGray);
        public static readonly BindableProperty ShadowBlurProperty = BindableProperty.Create(nameof(ShadowBlur), typeof(double), typeof(FancyFrame), GetShadowBlur());
        public static readonly BindableProperty ShadowSigmaProperty = BindableProperty.Create(nameof(ShadowSigma), typeof(double), typeof(FancyFrame), -6.0);        

        public static readonly BindableProperty BackgroundGradientStartColorProperty = BindableProperty.Create(nameof(BackgroundGradientStartColor), typeof(Color), typeof(FancyFrame), defaultValue: default(Color));
        public static readonly BindableProperty BackgroundGradientEndColorProperty = BindableProperty.Create(nameof(BackgroundGradientEndColor), typeof(Color), typeof(FancyFrame), defaultValue: default(Color));
        public static readonly BindableProperty BackgroundGradientAngleProperty = BindableProperty.Create(nameof(BackgroundGradientAngle), typeof(int), typeof(FancyFrame), defaultValue: default(int));
        public static readonly BindableProperty BackgroundGradientStopsProperty = BindableProperty.Create(nameof(BackgroundGradientStops), typeof(GradientStopCollection), typeof(FancyFrame), defaultValue: default(GradientStopCollection),
        defaultValueCreator: _ => new GradientStopCollection());

        public static readonly BindableProperty BorderGradientStartColorProperty = BindableProperty.Create(nameof(BorderGradientStartColor), typeof(Color), typeof(FancyFrame), defaultValue: default(Color));
        public static readonly BindableProperty BorderGradientEndColorProperty = BindableProperty.Create(nameof(BorderGradientEndColor), typeof(Color), typeof(FancyFrame), defaultValue: default(Color));
        public static readonly BindableProperty BorderGradientAngleProperty = BindableProperty.Create(nameof(BorderGradientAngle), typeof(int), typeof(FancyFrame), defaultValue: default(int));
        public static readonly BindableProperty BorderGradientStopsProperty = BindableProperty.Create(nameof(BorderGradientStops), typeof(GradientStopCollection), typeof(FancyFrame), defaultValue: default(GradientStopCollection),
        defaultValueCreator: _ => new GradientStopCollection());

        #endregion

        new public View Content
        {
            set
            {
                SetValue(ContentProperty, value);
            }
            get
            {
                return (View)GetValue(ContentProperty);
            }
        }

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public int BorderThickness
        {
            get { return (int)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        new public Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        public bool HasShadow
        {
            get { return (bool)GetValue(HasShadowProperty); }
            set { SetValue(HasShadowProperty, value); }
        }

        public double ShadowDistance
        {
            get => (double)GetValue(ShadowDistanceProperty);
            set => SetValue(ShadowDistanceProperty, value);
        }
       

        public double ShadowSigma
        {
            get => (double)GetValue(ShadowSigmaProperty);
            set => SetValue(ShadowSigmaProperty, value);
        }

        public double ShadowBlur
        {
            get => (double)GetValue(ShadowBlurProperty);
            set => SetValue(ShadowBlurProperty, value);
        }

        public Color LightShadowColor
        {
            get => (Color)GetValue(LightShadowColorProperty);
            set => SetValue(LightShadowColorProperty, value);
        }

        public Color DarkShadowColor
        {
            get => (Color)GetValue(DarkShadowColorProperty);
            set => SetValue(DarkShadowColorProperty, value);
        }

        public Color BackgroundGradientStartColor
        {
            get { return (Color)GetValue(BackgroundGradientStartColorProperty); }
            set { SetValue(BackgroundGradientStartColorProperty, value); }
        }

        public Color BackgroundGradientEndColor
        {
            get { return (Color)GetValue(BackgroundGradientEndColorProperty); }
            set { SetValue(BackgroundGradientEndColorProperty, value); }
        }

        public int BackgroundGradientAngle
        {
            get { return (int)GetValue(BackgroundGradientAngleProperty); }
            set { SetValue(BackgroundGradientAngleProperty, value); }
        }

        public GradientStopCollection BackgroundGradientStops
        {
            get { return (GradientStopCollection)GetValue(BackgroundGradientStopsProperty); }
            set { SetValue(BackgroundGradientStopsProperty, value); }
        }

        public Color BorderGradientStartColor
        {
            get { return (Color)GetValue(BorderGradientStartColorProperty); }
            set { SetValue(BorderGradientStartColorProperty, value); }
        }

        public Color BorderGradientEndColor
        {
            get { return (Color)GetValue(BorderGradientEndColorProperty); }
            set { SetValue(BorderGradientEndColorProperty, value); }
        }

        public int BorderGradientAngle
        {
            get { return (int)GetValue(BorderGradientAngleProperty); }
            set { SetValue(BorderGradientAngleProperty, value); }
        }

        public GradientStopCollection BorderGradientStops
        {
            get { return (GradientStopCollection)GetValue(BorderGradientStopsProperty); }
            set { SetValue(BorderGradientStopsProperty, value); }
        }

        public bool BorderIsDashed
        {
            get { return (bool)GetValue(BorderIsDashedProperty); }
            set { SetValue(BorderIsDashedProperty, value); }
        }

        public int Elevation
        {
            get { return (int)GetValue(ElevationProperty); }
            set { SetValue(ElevationProperty, value); }
        }

        private static BindableProperty.BindingPropertyChangedDelegate ContentPropertyChanged()
        {
            return async (bindableObject, _, newValue) =>
            {
                FancyFrame fancyFrame = bindableObject as FancyFrame;
                if (newValue is Image img)
                {
                    var stream = await ((StreamImageSource)img.Source).GetStreamAsync().ConfigureAwait(false);
                    fancyFrame.bitmap = SKBitmap.Decode(stream);
                    stream.Dispose();
                }
                else
                {
                    fancyFrame.rootGrid.Children.Add((View)newValue);
                }
            };
        }

        protected void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();

            int width = info.Width;
            int height = info.Height;
            float startX = 0;
            float startY = 0;

            if (HasShadow)
            {
                var newRectSize = DrawShadow(canvas, width, height);
                width = newRectSize.Width;
                height = newRectSize.Height;
                startX = newRectSize.Padding;
                startY = newRectSize.Padding;
            }

            //General Frame rect
            var skRect = new SKRect
            {
                Left = startX + BorderThickness,
                Top = startY + BorderThickness,
                Right = width - BorderThickness,
                Bottom = height - BorderThickness
            };

            SKRoundRect roundRect = new SKRoundRect(skRect);
            //Set Corner Radius                        
            SKPoint[] skPoints = new SKPoint[] { new SKPoint((float)CornerRadius.TopLeft * scale, (float)CornerRadius.TopLeft * scale), new SKPoint((float)CornerRadius.TopRight * scale, (float)CornerRadius.TopRight * scale), new SKPoint((float)CornerRadius.BottomRight * scale, (float)CornerRadius.BottomRight * scale), new SKPoint((float)CornerRadius.BottomLeft * scale, (float)CornerRadius.BottomLeft * scale) };
            roundRect.SetRectRadii(skRect, skPoints);

            if (BorderThickness > 0)
            {
                DrawBorder(canvas, width, height, roundRect);
            }

            DrawBackground(canvas, width, height, roundRect);

            if (bitmap != null)
            {
                canvas.DrawBitmap(bitmap, roundRect.Rect, BitmapStretch.AspectFill, BitmapAlignment.Center, BitmapAlignment.Center, null);
            }
        }

        private (int Width, int Height, int Padding) DrawShadow(SKCanvas canvas, int width, int height)
        {
            using (var shadowPaint = new SKPaint()
            {
                Style = SKPaintStyle.Fill,
                IsAntialias = true
            })
            {
                var fShadowDistance = Convert.ToSingle(ShadowDistance);
                var darkShadow = Color.FromRgba(DarkShadowColor.R, DarkShadowColor.G, DarkShadowColor.B, Elevation* 0.1 );
                var drawPadding = Convert.ToSingle(ShadowBlur*2);

                shadowPaint.MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, Convert.ToSingle(ShadowBlur));

                var diameter = drawPadding * 2;
                var rectangleWidth = width - diameter;
                var rectangleHeight = height - diameter;
                //update main rect               

                using (var path = CreatePath(rectangleWidth, rectangleHeight, drawPadding))
                {
                    shadowPaint.ImageFilter = darkShadow.ToSKDropShadow(fShadowDistance);                    
                    canvas.DrawPath(path, shadowPaint);
                    shadowPaint.ImageFilter = LightShadowColor.ToSKDropShadow(-fShadowDistance);                    
                    canvas.DrawPath(path, shadowPaint);
                }
                return ((int)rectangleWidth, (int)rectangleHeight, (int)drawPadding);
            }
        }

        private void DrawBackground(SKCanvas canvas, int width, int height, SKRoundRect roundRect)
        {
            using (var backgroundPaint = new SKPaint()
            {
                Color = BackgroundColor.ToSKColor(),
                Style = SKPaintStyle.Fill,
                IsAntialias = true
            })
            {
                if ((BackgroundGradientStartColor != default && BackgroundGradientEndColor != default) || (BackgroundGradientStops?.Any() == true))
                {
                    var angle = BackgroundGradientAngle / 360.0;
                    // Calculate the new positions based on angle between 0-360.
                    var a = width * Math.Pow(Math.Sin(2 * Math.PI * ((angle + 0.75) / 2)), 2);
                    var b = height * Math.Pow(Math.Sin(2 * Math.PI * ((angle + 0.0) / 2)), 2);
                    var c = width * Math.Pow(Math.Sin(2 * Math.PI * ((angle + 0.25) / 2)), 2);
                    var d = height * Math.Pow(Math.Sin(2 * Math.PI * ((angle + 0.5) / 2)), 2);

                    if (BackgroundGradientStops?.Count > 0)
                    {
                        // A range of colors is given. Let's add them.
                        var orderedStops = BackgroundGradientStops.OrderBy(x => x.Offset).ToList();
                        var colors = orderedStops.Select(x => x.Color.ToSKColor()).ToArray();
                        var locations = orderedStops.Select(x => x.Offset).ToArray();

                        backgroundPaint.Shader = SKShader.CreateLinearGradient(
                                   new SKPoint(width - (float)a, (float)b),
                                   new SKPoint(width - (float)c, (float)d),
                                   colors,
                                   locations,
                                   SKShaderTileMode.Clamp);
                    }
                    else
                    {
                        backgroundPaint.Shader = SKShader.CreateLinearGradient(
                                   new SKPoint(width - (float)a, (float)b),
                                   new SKPoint(width - (float)c, (float)d),
                                   new SKColor[] { BackgroundGradientStartColor.ToSKColor(), BackgroundGradientEndColor.ToSKColor() },
                                   null,
                                   SKShaderTileMode.Clamp);
                    }
                }

                canvas.DrawRoundRect(roundRect, backgroundPaint);
                canvas.ClipRoundRect(roundRect, SKClipOperation.Intersect);
            }
        }

        private void DrawBorder(SKCanvas canvas, int width, int height, SKRoundRect roundRect)
        {
            float strokeWidth = 1;
            switch (Device.RuntimePlatform)
            {
                case Device.WPF:
                case Device.GTK:
                case Device.UWP:
                case Device.macOS:
                    strokeWidth = BorderThickness * scale * 1.5f;
                    break;
                default:
                    strokeWidth = BorderThickness * scale;
                    break;
            }
            using (SKPaint borderPaint = new SKPaint()
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = strokeWidth,
                Color = BorderColor.ToSKColor(),
                IsAntialias = true
            })
            {
                //set gradients

                if ((BorderGradientStartColor != default && BorderGradientEndColor != default) || (BorderGradientStops?.Count > 0))
                {
                    var angle = BorderGradientAngle / 360.0;
                    // Calculate the new positions based on angle between 0-360.
                    var a = width * Math.Pow(Math.Sin(2 * Math.PI * ((angle + 0.75) / 2)), 2);
                    var b = height * Math.Pow(Math.Sin(2 * Math.PI * ((angle + 0.0) / 2)), 2);
                    var c = width * Math.Pow(Math.Sin(2 * Math.PI * ((angle + 0.25) / 2)), 2);
                    var d = height * Math.Pow(Math.Sin(2 * Math.PI * ((angle + 0.5) / 2)), 2);

                    if (BorderGradientStops?.Count > 0)
                    {
                        // A range of colors is given. Let's add them.
                        var borderOrderedStops = BorderGradientStops.OrderBy(x => x.Offset).ToList();
                        var borderColors = borderOrderedStops.Select(x => x.Color.ToSKColor()).ToArray();
                        var borderlocations = borderOrderedStops.Select(x => x.Offset).ToArray();

                        borderPaint.Shader = SKShader.CreateLinearGradient(
                                   new SKPoint(width - (float)a, (float)b),
                                   new SKPoint(width - (float)c, (float)d),
                                   borderColors,
                                   borderlocations,
                                   SKShaderTileMode.Clamp);
                    }
                    else
                    {
                        borderPaint.Shader = SKShader.CreateLinearGradient(
                                   new SKPoint(width - (float)a, (float)b),
                                   new SKPoint(width - (float)c, (float)d),
                                   new SKColor[] { BorderGradientStartColor.ToSKColor(), BorderGradientEndColor.ToSKColor() },
                                   null,
                                   SKShaderTileMode.Clamp);
                    }
                }

                // dashes merge when thickness is increased
                // off-distance should be scaled according to thickness                   
                if (BorderIsDashed)
                {
                    switch (Device.RuntimePlatform)
                    {
                        case Device.Android:
                            borderPaint.PathEffect = SKPathEffect.CreateDash(new float[] { 10 * scale, 5 * (float)BorderThickness / scale }, 0);
                            break;
                        case Device.UWP:
                        case Device.WPF:
                        case Device.GTK:
                        case Device.Tizen:
                            borderPaint.PathEffect = SKPathEffect.CreateDash(new float[] { 10 * scale, BorderThickness }, 0);
                            break;
                        case Device.iOS:
                            borderPaint.PathEffect = SKPathEffect.CreateDash(new float[] { 10 * scale, 5 * (float)BorderThickness / scale }, 0);
                            break;
                        case Device.macOS:
                            borderPaint.PathEffect = SKPathEffect.CreateDash(new float[] { 10 * scale, BorderThickness / scale }, 0);
                            break;
                        default:
                            borderPaint.PathEffect = SKPathEffect.CreateDash(new float[] { 10 * scale, 5 * (float)BorderThickness / scale }, 0);
                            break;
                    }
                }
                canvas.DrawRoundRect(roundRect, borderPaint);
            }
        }

        private SKPath CreatePath(float retangleWidth, float retangleHeight, float drawPadding)
        {
            var path = new SKPath();
            var fTopLeftRadius = Convert.ToSingle(CornerRadius.TopLeft);
            var fTopRightRadius = Convert.ToSingle(CornerRadius.TopRight);
            var fBottomLeftRadius = Convert.ToSingle(CornerRadius.BottomLeft);
            var fBottomRightRadius = Convert.ToSingle(CornerRadius.BottomRight);

            var startX = fTopLeftRadius + drawPadding;
            var startY = drawPadding;

            path.MoveTo(startX, startY);

            path.LineTo(retangleWidth - fTopRightRadius + drawPadding, startY);
            path.ArcTo(fTopRightRadius,
                new SKPoint(retangleWidth + drawPadding, fTopRightRadius + drawPadding));

            path.LineTo(retangleWidth + drawPadding, retangleHeight - fBottomRightRadius + drawPadding);
            path.ArcTo(fBottomRightRadius,
                 new SKPoint(retangleWidth - fBottomRightRadius + drawPadding, retangleHeight + drawPadding));

            path.LineTo(fBottomLeftRadius + drawPadding, retangleHeight + drawPadding);
            path.ArcTo(fBottomLeftRadius,
                new SKPoint(drawPadding, retangleHeight - fBottomLeftRadius + drawPadding));

            path.LineTo(drawPadding, fTopLeftRadius + drawPadding);
            path.ArcTo(fTopLeftRadius, new SKPoint(startX, startY));

            path.Close();

            return path;
        }

        private static double GetShadowDistance()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    return 5.0;
                case Device.UWP:
                case Device.WPF:
                case Device.GTK:
                case Device.Tizen:
                case Device.macOS:
                    return 2.0;
                case Device.iOS:
                    return 3.0;
                default:
                    return 5.0; 
            }           
        }

        private static double GetShadowBlur()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    return 10.0;
                case Device.UWP:
                case Device.WPF:
                case Device.GTK:
                case Device.Tizen:
                case Device.macOS:
                    return 4.0;
                case Device.iOS:
                    return 6.0;
                default:
                    return 5.0;
            }
        }

        private static int GetElavation()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    return 7;
                case Device.UWP:
                case Device.WPF:
                case Device.GTK:
                case Device.Tizen:
                    return 6;
                default:
                    return 6;
            }
        }


        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            // Determine when to change. Basically on any of the properties that we've added that affect
            // the visualization, including the size of the control, we'll repaint
            if (propertyName == BorderColorProperty.PropertyName ||
                propertyName == BorderThicknessProperty.PropertyName ||
                propertyName == BorderGradientStartColorProperty.PropertyName ||
                propertyName == BorderGradientEndColorProperty.PropertyName ||
                propertyName == BorderGradientAngleProperty.PropertyName ||
                propertyName == BorderGradientStopsProperty.PropertyName ||
                propertyName == BorderIsDashedProperty.PropertyName ||
                propertyName == BorderDrawingStyleProperty.PropertyName ||
                propertyName == CornerRadiusProperty.PropertyName ||
                propertyName == ElevationProperty.PropertyName ||
                propertyName == HasShadowProperty.PropertyName ||
                propertyName == LightShadowColorProperty.PropertyName ||
                propertyName == DarkShadowColorProperty.PropertyName ||
                propertyName == ShadowDistanceProperty.PropertyName ||                
                propertyName == ShadowBlurProperty.PropertyName ||
                propertyName == ShadowSigmaProperty.PropertyName ||                
                propertyName == BackgroundColorProperty.PropertyName ||
                propertyName == BackgroundGradientStartColorProperty.PropertyName ||
                propertyName == BackgroundGradientEndColorProperty.PropertyName ||
                propertyName == BackgroundGradientAngleProperty.PropertyName ||
                propertyName == BackgroundGradientStopsProperty.PropertyName ||
                propertyName == ContentProperty.PropertyName)
            {
                canvas.InvalidateSurface();
            }
        }

        public void Dispose()
        {
            canvas.PaintSurface -= OnPaintSurface;
            bitmap?.Dispose();
        }

        public void OnInvalidateSurvface()
        {
            canvas.InvalidateSurface();
        }
    }
}
