using FancyFrameApp.Extensions;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace FancyFrameApp.Control
{
    public class FancyFrame : ContentView, IDisposable
    {
        private readonly Grid contentGrid = new Grid();
        private readonly SKCanvasView canvas;
        private readonly float scale;
        private SKBitmap bitmap;
        public FancyFrame()
        {
             scale = Device.info.ScalingFactor == 0 ? 1 : (float)Device.info.ScalingFactor;
            canvas = new SKCanvasView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            canvas.PaintSurface += OnPaintSurface;
            Grid grid = new Grid();
            grid.Children.Add(canvas);

            contentGrid.Margin = new Thickness(10.5);
            grid.Children.Add(contentGrid);

            base.Content = grid;
        }

        #region Properties

        new public static readonly BindableProperty ContentProperty = BindableProperty.Create(nameof(Content), typeof(View), typeof(FancyFrame),
                                                                                                    defaultValue: null,
                                                                                                    propertyChanged: async (bindableObject, _, newValue) =>
                                                                                                    {
                                                                                                        FancyFrame shadowView = bindableObject as FancyFrame;
                                                                                                        if ((View)newValue is Image img)
                                                                                                        {
                                                                                                            var stream = await ((StreamImageSource)img.Source).GetStreamAsync().ConfigureAwait(false);
                                                                                                            shadowView.bitmap = SKBitmap.Decode(stream);
                                                                                                            stream.Dispose();
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            shadowView.contentGrid.Children.Add((View)newValue);
                                                                                                        }                                                                                                        
                                                                                                    });
        new public static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(FancyFrame), Color.White);
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(FancyFrame), Color.Black);
        public static readonly BindableProperty BorderThicknessProperty = BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(FancyFrame), 0);        
        public static readonly BindableProperty BorderIsDashedProperty = BindableProperty.Create(nameof(BorderIsDashed), typeof(bool), typeof(FancyFrame), default(bool));
        public static readonly BindableProperty BorderDrawingStyleProperty = BindableProperty.Create(nameof(BorderDrawingStyle), typeof(BorderDrawingStyle), typeof(FancyFrame), defaultValue: BorderDrawingStyle.Inside);

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(FancyFrame), default(CornerRadius));
        public static readonly BindableProperty HasShadowProperty = BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(FancyFrame), default(bool));
        public static readonly BindableProperty ShadowColorProperty = BindableProperty.Create(nameof(ShadowColor), typeof(Color), typeof(FancyFrame), Color.Black);
        public static readonly BindableProperty ElevationProperty = BindableProperty.Create(nameof(Elevation), typeof(int), typeof(FancyFrame), 0);

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

        public static readonly BindableProperty TempProperty = BindableProperty.Create(nameof(Temp), typeof(float), typeof(FancyFrame), 20f);

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

        public Color ShadowColor
        {
            get { return (Color)GetValue(ShadowColorProperty); }
            set { SetValue(ShadowColorProperty, value); }
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

        public float Temp
        {
            get { return (float)GetValue(TempProperty); }
            set { SetValue(TempProperty, value); }
        }

        protected void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {

            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;                        
            canvas.Clear();

            int width = info.Width;
            int height = info.Height;

            //General Frame rect
            var skRect = new SKRect
            {
                Left = BorderThickness,
                Top = BorderThickness,
                Right = width - BorderThickness,
                Bottom = height - BorderThickness
            };

            SKRoundRect roundRect = new SKRoundRect(skRect);
            //Set Corner Radius                        
            SKPoint[] skPoints = new SKPoint[] { new SKPoint((float)CornerRadius.TopLeft * scale, (float)CornerRadius.TopLeft * scale), new SKPoint((float)CornerRadius.TopRight * scale, (float)CornerRadius.TopRight * scale), new SKPoint((float)CornerRadius.BottomRight * scale, (float)CornerRadius.BottomRight * scale), new SKPoint((float)CornerRadius.BottomLeft * scale, (float)CornerRadius.BottomLeft * scale) };
            roundRect.SetRectRadii(skRect, skPoints);

            #region Border
            if (BorderThickness > 0)
            {
                float strokeWidth = 1;
                switch (Device.RuntimePlatform)
                {
                    case Device.WPF: case Device.GTK: case Device.UWP:
                        strokeWidth = BorderThickness * scale * 2;
                        break;
                    default:
                        strokeWidth = BorderThickness * scale;
                        break;
                }
                SKPaint borderPaint = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = strokeWidth,                    
                    Color = BorderColor.ToSKColor(),
                    IsAntialias = true
                };

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
                        case Device.UWP: case Device.WPF: case Device.GTK:
                            borderPaint.PathEffect = SKPathEffect.CreateDash(new float[] { 20 * scale, BorderThickness }, 0);
                            break;
                        case Device.iOS:                           
                            borderPaint.PathEffect = SKPathEffect.CreateDash(new float[] { 10 * scale, 5 * (float)BorderThickness / scale }, 0);
                            break;                       
                        default:                           
                            borderPaint.PathEffect = SKPathEffect.CreateDash(new float[] { 10 * scale, 5 * (float)BorderThickness / scale }, 0);
                            break;
                    }
                }                
                canvas.DrawRoundRect(roundRect, borderPaint);
            }
            #endregion
            
            #region Background
            var backgroundPaint = new SKPaint()
            {
                Color = BackgroundColor.ToSKColor(),
                Style = SKPaintStyle.Fill,
                IsAntialias = true,
                ImageFilter = HasShadow ? SKImageFilter.CreateDropShadow(0f, 0f, Temp, Temp, ShadowColor.ToSKColor(), SKDropShadowImageFilterShadowMode.DrawShadowAndForeground, null, null) : null
            };

            //Set Gradients
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

            #endregion
            
            canvas.DrawRoundRect(roundRect, backgroundPaint);
            canvas.ClipRoundRect(roundRect, SKClipOperation.Intersect);

            #region Draw Bitmap
           
            if (bitmap != null)
            {
             canvas.DrawBitmap(bitmap, roundRect.Rect,BitmapStretch.AspectFill, BitmapAlignment.Center, BitmapAlignment.Center, null);
            }

            #endregion

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
                propertyName == ShadowColorProperty.PropertyName ||
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
    }
}
