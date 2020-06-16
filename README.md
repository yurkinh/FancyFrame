<img src="https://github.com/yurkinh/FancyFrame/blob/master/Images/fancyframe.png" width="150px" />

# FancyFrame
A SkiaSharp based frame control with advanced capabilities

## Purpose of FancyFrame

This is Proof of Concept for rewriting with SkiaSharp one of most popular (and one of my favourite :) 
third party Xamarin.Forms component named ```PancakeView``` by Steve Thewissen .

PancakeView for iOS (Left)  and FancyFrame with Skiasharp (right)      
<img src="https://github.com/yurkinh/FancyFrame/blob/master/Images/iosOrigGif.gif" width="400px" />
<img src="https://github.com/yurkinh/FancyFrame/blob/master/Images/iosGif.gif" width="400px" />

PancakeView for Android (Left)  and FancyFrame with Skiasharp (right)      
<img src="https://github.com/yurkinh/FancyFrame/blob/master/Images/AndroidOrigGif.gif" width="400px" />
<img src="https://github.com/yurkinh/FancyFrame/blob/master/Images/AndroidGif.gif" width="400px" />

I have used both test pages from original component and combined it into one.




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
All property names were taken from original component. Also there are some new ones.
I have tuned all properties to look like original component. 
Tizen controls are a bit squeezed due to low emulator resolution.

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
| `LightShadowColor` | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| `DarkShadowColor` | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| `ShadowBlur` | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| `ShadowDistance` | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| `ShadowSigma` | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| `Source` | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
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
| `CornerRadius` | A `CornerRadius` object representing each individual corner's radius. | Uses the `CornerRadius` struct allowing you to specify individual corners.|
| `HasShadow` | Whether or not to draw a shadow beneath the control. |  |
| `Elevation` | The Material Design elevation desired. | |
| `OffsetAngle` | The rotation of the `FancyFrame` when `Sides` is not its default value of 4. |  |
| `Sides` | The amount of sides to the shape. | Changes the `FancyFrame` from being 4-sided to what you provide here. |
| `LightShadowColor` | Draw top left shadow|
| `DarkShadowColor` | Draw bottom right shadow|
| `ShadowBlur` | Shadow bluring |
| `ShadowDistance` | This is shadow width actually |
| `Source` | Image source | Renders an image within frame with clipped bounds |

