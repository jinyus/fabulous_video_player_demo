namespace demo

open Fabulous
open Fabulous.Avalonia

// Tells Fabulous that VideoView inherits all the same modifiers than Control
// eg. BackgroundColor, Width, Height, etc.
type IFabVideoView = inherit Fabulous.Avalonia.IFabControl

module VideoView =
    open LibVLCSharp.Avalonia
    
    // Register the new widget with Fabulous
    // This tells Fabulous that if you use the this VideoView widget, it needs to instantiate a LibVLCSharp.Avalonia.VideoView control
    let WidgetKey = Widgets.register<LibVLCSharp.Avalonia.VideoView>()

    // Declare the properties you will set on the widget
    // Here VideoView has only one: MediaPlayer
    let MediaPlayer = Attributes.defineAvaloniaPropertyWithEquality VideoView.MediaPlayerProperty

// Define the available constructors for the widget we are creating
// Here VideoView only makes sense if we provide the MediaPlayer so let's make it mandatory in the constructor
[<AutoOpen>]
module VideoViewBuilders =
    open LibVLCSharp.Shared
    open Fabulous
    type Fabulous.Avalonia.View with
        static member inline VideoView<'msg>(mediaPlayer: MediaPlayer) =
            WidgetBuilder<'msg, IFabVideoView>(
                VideoView.WidgetKey,
                VideoView.MediaPlayer.WithValue(mediaPlayer)
            )