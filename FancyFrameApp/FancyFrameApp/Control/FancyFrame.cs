using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace FancyFrameApp.Control
{
    public class FancyFrame : Grid
    {
        readonly SKCanvasView canvas;
        public FancyFrame()
        {
            RowDefinitions = new RowDefinitionCollection { new RowDefinition { Height = GridLength.Auto } };
            ColumnDefinitions = new ColumnDefinitionCollection { new ColumnDefinition { Width = GridLength.Auto } };

            canvas = new SKCanvasView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,

            };
            canvas.PaintSurface += OnPaintSurface;
            Children.Add(canvas, 0, 0);
        }

        public static readonly BindableProperty BGColorProperty =
           BindableProperty.Create(nameof(BGColor), typeof(Color), typeof(FancyFrame), Color.Transparent);

        public Color BGColor
        {
            get { return (Color)GetValue(BGColorProperty); }
            set { SetValue(BGColorProperty, value); }
        }

        
        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color),typeof(FancyFrame), Color.Black);

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }
        
        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth),typeof(int), typeof(FancyFrame), 1);

        public int BorderWidth
        {
            get { return (int)GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }
        
        public static readonly BindableProperty BorderRadiusProperty = BindableProperty.Create(nameof(BorderRadius),typeof(float), typeof(FancyFrame),25f );

        public float BorderRadius
        {
            get { return (float)GetValue(BorderRadiusProperty); }
            set { SetValue(BorderRadiusProperty, value); }
        }
        
        public static readonly BindableProperty BorderMarginProperty = BindableProperty.Create(nameof(BorderMargin), typeof(float), typeof(FancyFrame),25f );

        public float BorderMargin
        {
            get { return (float)GetValue(BorderMarginProperty); }
            set { SetValue(BorderMarginProperty, value); }
        }
        
        public static readonly BindableProperty BorderPaddingProperty = BindableProperty.Create(nameof(BorderPadding), typeof(float), typeof(FancyFrame), 25f );

        public float BorderPadding
        {
            get { return (float)GetValue(BorderPaddingProperty); }
            set { SetValue(BorderPaddingProperty, value); }
        }       

        new public static readonly BindableProperty BackgroundColorProperty =
                    BindableProperty.Create(nameof(BackgroundColor), typeof(Color),
                        typeof(FancyFrame), Color.Transparent);

        new public Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        protected override void OnChildAdded(Element child)
        {
            if (Children.Count > 1)
            {
                SetRow(child, 0);
                SetColumn(child, 0);
                SetRowSpan(child, 1);
                SetColumnSpan(child, 1);
                //((View)child).Margin = BorderMargin + BorderWidth + BorderPadding;
                ((View)child).Margin = BorderMargin + BorderPadding;
            }
            base.OnChildAdded(child);
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
            canvas.DrawRoundRect(rect, BorderRadius, BorderRadius, paintBorder);
            

            var paintFill = new SKPaint()
            {      
                Color=BackgroundColor.ToSKColor(),
                Style = SKPaintStyle.Fill,
                IsAntialias = true
            };

            canvas.DrawRoundRect(rect, BorderRadius, BorderRadius, paintFill);
            canvas.ClipRect(rect, SKClipOperation.Intersect);
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            // Determine when to change. Basically on any of the properties that we've added that affect
            // the visualization, including the size of the control, we'll repaint
            if (propertyName == BGColorProperty.PropertyName ||
                propertyName == BorderColorProperty.PropertyName ||
                propertyName == BorderWidthProperty.PropertyName ||
                propertyName == BorderRadiusProperty.PropertyName ||
                propertyName == BorderPaddingProperty.PropertyName ||
                propertyName == BackgroundColorProperty.PropertyName )
            {
                canvas.InvalidateSurface();
            }
        }
    }
}
