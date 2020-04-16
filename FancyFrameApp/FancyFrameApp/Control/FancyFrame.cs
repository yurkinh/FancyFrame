using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using Xamarin.Forms;

namespace FancyFrameApp.Control
{
    public class FancyFrame : ContentView, IDisposable
    {
        readonly Grid contentGrid = new Grid();
        readonly SKCanvasView canvas;
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

            Content = grid;
        }

        #region Properties
        
        public static readonly BindableProperty ContainerContentProperty = BindableProperty.Create(nameof(ContainerContent), typeof(View), typeof(FancyFrame),
                                                                                                    defaultValue: null,
                                                                                                    propertyChanged: (bindableObject, oldValue, newValue) =>
                                                                                                    {
                                                                                                        FancyFrame shadowView = bindableObject as FancyFrame;
                                                                                                        shadowView.contentGrid.Children.Add((View)newValue);
                                                                                                    });
        new public static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(FancyFrame), Color.Transparent);
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(FancyFrame), Color.Black);
        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(int), typeof(FancyFrame), 1);
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(float), typeof(FancyFrame), 25f);
        public static readonly BindableProperty BorderMarginProperty = BindableProperty.Create(nameof(BorderMargin), typeof(float), typeof(FancyFrame), 25f);
        public static readonly BindableProperty BorderPaddingProperty = BindableProperty.Create(nameof(BorderPadding), typeof(float), typeof(FancyFrame), 25f);
        public static readonly BindableProperty HasShadowProperty = BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(FancyFrame), default(bool));
        public static readonly BindableProperty ShadowColorProperty = BindableProperty.Create(nameof(ShadowColor), typeof(Color), typeof(FancyFrame), Color.Black);



        #endregion

        public View ContainerContent
        {
            set
            {
                SetValue(ContainerContentProperty, value);
            }
            get
            {
                return (View)GetValue(ContainerContentProperty);
            }
        }

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }        

        public int BorderWidth
        {
            get { return (int)GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }

        public float CornerRadius
        {
            get { return (float)GetValue(CornerRadiusProperty); }
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

        protected void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            int width = info.Width;
            int height = info.Height;

            SKRect rect = new SKRect
            {
                Left = BorderMargin,
                Top = BorderMargin,
                Right = width - BorderMargin,
                Bottom = height - BorderMargin
            };            
            

            SKPaint paintBorder = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = BorderWidth,
                Color = BorderColor.ToSKColor(),
                IsAntialias = true
            };
            canvas.DrawRoundRect(rect, CornerRadius, CornerRadius, paintBorder);


            var paintFill = new SKPaint()
            {
                Color = BackgroundColor.ToSKColor(),
                Style = SKPaintStyle.Fill,
                IsAntialias = true,
                ImageFilter = HasShadow ? SKImageFilter.CreateDropShadow(0f, 0f, 20f, 20f, ShadowColor.ToSKColor(), SKDropShadowImageFilterShadowMode.DrawShadowAndForeground, null, null) : null
            };

            canvas.DrawRoundRect(rect, CornerRadius, CornerRadius, paintFill);
            canvas.ClipRect(rect, SKClipOperation.Intersect);
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            // Determine when to change. Basically on any of the properties that we've added that affect
            // the visualization, including the size of the control, we'll repaint
            if (propertyName == BorderColorProperty.PropertyName ||
                propertyName == BorderWidthProperty.PropertyName ||
                propertyName == CornerRadiusProperty.PropertyName ||
                propertyName == BorderPaddingProperty.PropertyName ||
                propertyName == BackgroundColorProperty.PropertyName ||
                propertyName == HasShadowProperty.PropertyName ||
                propertyName == ShadowColorProperty.PropertyName)
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
