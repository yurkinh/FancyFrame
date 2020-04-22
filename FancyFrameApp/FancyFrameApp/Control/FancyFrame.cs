using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Linq;
using Xamarin.Forms;

namespace FancyFrameApp.Control
{
    public class FancyFrame : ContentView, IDisposable
    {
        private readonly Grid contentGrid = new Grid();
        private readonly SKCanvasView canvas;
        public FancyFrame()
        {            
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
                                                                                                    propertyChanged: (bindableObject, _, newValue) =>
                                                                                                    {
                                                                                                        FancyFrame shadowView = bindableObject as FancyFrame;                                                                                                        
                                                                                                        shadowView.contentGrid.Children.Add((View)newValue);
                                                                                                    });
        new public static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(FancyFrame), Color.White);
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(FancyFrame), Color.Black);
        public static readonly BindableProperty BorderThicknessProperty = BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(FancyFrame), 0);
        public static readonly BindableProperty BorderMarginProperty = BindableProperty.Create(nameof(BorderMargin), typeof(float), typeof(FancyFrame), 25f);
        public static readonly BindableProperty BorderPaddingProperty = BindableProperty.Create(nameof(BorderPadding), typeof(float), typeof(FancyFrame), 25f);

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(FancyFrame), default(CornerRadius));
        public static readonly BindableProperty HasShadowProperty = BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(FancyFrame), default(bool));
        public static readonly BindableProperty ShadowColorProperty = BindableProperty.Create(nameof(ShadowColor), typeof(Color), typeof(FancyFrame), Color.Black);

        public static readonly BindableProperty BackgroundGradientStartColorProperty = BindableProperty.Create(nameof(BackgroundGradientStartColor), typeof(Color), typeof(FancyFrame), defaultValue: default(Color));
        public static readonly BindableProperty BackgroundGradientEndColorProperty = BindableProperty.Create(nameof(BackgroundGradientEndColor), typeof(Color), typeof(FancyFrame), defaultValue: default(Color));
        public static readonly BindableProperty BackgroundGradientAngleProperty = BindableProperty.Create(nameof(BackgroundGradientAngle), typeof(int), typeof(FancyFrame), defaultValue: default(int));
        public static readonly BindableProperty BackgroundGradientStopsProperty = BindableProperty.Create(nameof(BackgroundGradientStops), typeof(GradientStopCollection), typeof(FancyFrame), defaultValue: default(GradientStopCollection),
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

        public float BorderMargin
        {
            get { return (float)GetValue(BorderMarginProperty); }
            set { SetValue(BorderMarginProperty, value); }
        }

        public float BorderPadding
        {
            get { return (float)GetValue(BorderPaddingProperty); }
            set { SetValue(BorderPaddingProperty, value); }
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
                Left = BorderMargin,
                Top = BorderMargin,
                Right = width - BorderMargin,
                Bottom = height - BorderMargin
            };

            SKRoundRect rect = new SKRoundRect(skRect);
            //Set Corner Radius
            var skPoints = new SKPoint[] { new SKPoint((float)CornerRadius.TopLeft, (float)CornerRadius.TopLeft), new SKPoint((float)CornerRadius.TopRight, (float)CornerRadius.TopRight), new SKPoint((float)CornerRadius.BottomRight, (float)CornerRadius.BottomRight), new SKPoint((float)CornerRadius.BottomLeft, (float)CornerRadius.BottomLeft) };
            rect.SetRectRadii(skRect, skPoints);

            #region Border
            if (BorderThickness > 0)
            {
                SKPaint paintBorder = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = BorderThickness,
                    Color = BorderColor.ToSKColor(),
                    IsAntialias = true
                };
                canvas.DrawRoundRect(rect, paintBorder);
            }
            #endregion

            var backgroundPaint = new SKPaint()
            {
                Color = BackgroundColor.ToSKColor(),
                Style = SKPaintStyle.Fill,
                IsAntialias = true,
                ImageFilter = HasShadow ? SKImageFilter.CreateDropShadow(0f, 0f, 20f, 20f, ShadowColor.ToSKColor(), SKDropShadowImageFilterShadowMode.DrawShadowAndForeground, null, null) : null
            };

            //Set Gradients
            if ((BackgroundGradientStartColor != default(Color) && BackgroundGradientEndColor != default(Color)) || (BackgroundGradientStops != null && BackgroundGradientStops.Any()))
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

            canvas.DrawRoundRect(rect, backgroundPaint);            
            canvas.ClipRoundRect(rect, SKClipOperation.Intersect);
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            // Determine when to change. Basically on any of the properties that we've added that affect
            // the visualization, including the size of the control, we'll repaint
            if (propertyName == BorderColorProperty.PropertyName ||
                propertyName == BorderThicknessProperty.PropertyName ||
                propertyName == BorderPaddingProperty.PropertyName ||
                propertyName == BorderMarginProperty.PropertyName ||
                propertyName == CornerRadiusProperty.PropertyName ||
                propertyName == HasShadowProperty.PropertyName ||
                propertyName == ShadowColorProperty.PropertyName ||
                propertyName == BackgroundColorProperty.PropertyName ||
                propertyName == BackgroundGradientStartColorProperty.PropertyName ||
                propertyName == BackgroundGradientEndColorProperty.PropertyName ||
                propertyName == BackgroundGradientAngleProperty.PropertyName ||
                propertyName == ContentProperty.PropertyName)
            {
                canvas.InvalidateSurface();
            }
        }

        public void Dispose()
        {
            canvas.PaintSurface -= OnPaintSurface;
        }
    }
}
