namespace demo

open Fabulous
open Fabulous.Avalonia

open type Fabulous.Avalonia.View

module App =
    open LibVLCSharp.Shared
    open System
    // Initialize the media player with the media source
    // Note that all objects are disposable but here for brevity I don't dispose them
    let mediaPlayer =
        let libVLC = new LibVLC()
        let media = new Media(libVLC, new Uri(@"/home/dev/videos/samples/1.mp4"))
        new MediaPlayer(media)

    type Model =
        { IsPlaying: bool }

    type Msg =
        | Play
        | Stop

    let init () = { IsPlaying = false }, Cmd.none

    let update msg model =
        match msg with
        | Play ->
            mediaPlayer.Play() |> ignore
            { model with IsPlaying = true }, Cmd.none
    
        | Stop ->
            mediaPlayer.Stop()
            { model with IsPlaying = false }, Cmd.none

    let view model =
        VStack() {
            Button("Play", Play)
            Button("Stop", Stop)
            VideoView(mediaPlayer)
        }

    
    let app model = DesktopApplication(Window(view model))

    
    let program = Program.statefulWithCmd init update app