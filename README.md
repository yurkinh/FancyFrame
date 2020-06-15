<img src="https://github.com/yurkinh/FancyFrame/blob/master/Images/fancyframe.png" width="150px" />

# FancyFrame
A SkiaSharp based frame control with advanced capabilities

## Purpose of FancyFrame

This is Proof of Concept for rewriting with SkiaSharp one of most popular (and one of my favourite :) 
third party Xamarin.Forms component named ```PancakeView``` by Steve Thewissen .

Original component for iOS (Left)  and rewritten with Skiasharp (right)      
<img src="https://github.com/sthewissen/Xamarin.Forms.PancakeView/blob/master/images/pancake.gif" width="400px" />
<img src="https://github.com/yurkinh/FancyFrame/blob/master/Images/iosGif.gif" width="400px" />

I have used most recent test pages from original component and combined it into one




## Platform support

Android and iOS

<img src="https://github.com/yurkinh/FancyFrame/blob/master/Images/AndroidGif.gif" width="400px" />  <img src="https://github.com/yurkinh/FancyFrame/blob/master/Images/iosGif.gif" width="400px" />

UWP and WPF

<img src="https://github.com/yurkinh/FancyFrame/blob/master/Images/UWPGif.gif" width="400px" />  <img src="https://github.com/yurkinh/FancyFrame/blob/master/Images/WPFGif.gif" width="400px" />

MacOS and GTK

<img src="https://github.com/yurkinh/FancyFrame/blob/master/Images/MacOsGif.gif" width="400px" />  <img src="https://github.com/yurkinh/FancyFrame/blob/master/Images/GtkGif.Gif" width="400px" />

Tizen

<img src="https://github.com/yurkinh/FancyFrame/blob/master/Images/TizenGif.gif" width="400px" /> 

I have added all Xamarin.Forms supported platforms.
I don't know whether this table makes any sense due to implementation specifics :)
Suggestions and Pr's are very wellcome ☺️

| Property | iOS | Android | UWP | WPF | MacOS | Tizen | Gtk |
| ------ | ------ | ------ | ------ | ------ |------ | ------ | ------ |
| `BackgroundGradientAngle` | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| `BackgroundGradientStartColor` | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| `BackgroundGradientEndColor` | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| `BackgroundGradientStops` | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| `BorderColor` | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| `BorderGradientAngle` | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| `BorderGradientStartColor` | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| `BorderGradientEndColor` | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| `BorderGradientStops` | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| `BorderIsDashed` | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| `BorderDashPattern` | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| `BorderThickness` | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| `CornerRadius` | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| `HasShadow` | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| `Elevation` | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| `OffsetAngle` | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| `Sides` | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| `ClipToBounds` | ❌ | ❌ | ❌| ❌ | ❌ | ❌ | ❌|



## Property reference

| Property | What it does | Extra info |
| ------ | ------ | ------ |
| `BackgroundGradientAngle` | A value between 0-360 to define the angle of the background gradient. | |
| `BackgroundGradientStartColor` | The start color of the background gradient. | A ```Color``` object. |
| `BackgroundGradientEndColor` | The end color of the background gradient. | A ```Color``` object. |
| `BackgroundGradientStops` | A list of `GradientStop` objects that define a multi color gradient. | `Offset` is a value between 0-1 defining the location within the gradient. |
| `BorderColor` | The color of the border. | A ```Color``` object. |
| `BorderGradientAngle` | A value between 0-360 to define the angle of the border gradient. | |
| `BorderGradientStartColor` | The start color of the border gradient. | A ```Color``` object. |
| `BorderGradientEndColor` | The end color of the border gradient. | A ```Color``` object. |
| `BorderGradientStops` | A list of `GradientStop` objects that define a multi color gradient. | `Offset` is a value between 0-1 defining the location within the gradient. |
| `BorderIsDashed` | Whether or not the border needs to be dashed. | The length of the dash and spacing between them is currently not editable. |
| `BorderThickness` | The thickness of the border. | |
| `CornerRadius` | A `CornerRadius` object representing each individual corner's radius. | Uses the `CornerRadius` struct allowing you to specify individual corners. This does have some drawbacks. |
| `HasShadow` | Whether or not to draw a shadow beneath the control. | For this to work we need to clip the view. This means that individual corner radii will be lost. In this case the `TopLeft` value will be used for all corners. |
| `Elevation` | The Material Design elevation desired. | For this to work we need to clip the view. This means that individual corner radii will be lost. In this case the `TopLeft` value will be used for all corners. |
| `OffsetAngle` | The rotation of the `PancakeView` when `Sides` is not its default value of 4. |  |
| `Sides` | The amount of sides to the shape. | Changes the `PancakeView` from being 4-sided to what you provide here. |

