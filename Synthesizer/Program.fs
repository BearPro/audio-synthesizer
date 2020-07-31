open System
open System.IO

open WavFile
open BaseLib
open FxLib

[<EntryPoint>]
let main argv =
    use stream = openStream "sample.wav"





    let w = wavefun

    let w2 freq = wavefun (freq * 2.0)

    let line1 = 
        wavefun
        |> linearDrop 12000.0
        |> repeat 16000.0
        |> freezeFreq 440.0
        

    let line2 =
        let fx = wavefun
        let fx = linearDrop 1000.0 fx
        let fx = repeat 4000.0 fx
        let fx = freezeFreq 220.0 fx
        fx

    let line3 = 
        let fx = wavefun
        let fx = linearDrop 2000.0 fx
        let fx = repeat 4000.0 fx
        let fx = offset 6000.0 fx
        let fx = freezeFreq (440.0 ** (2.0 - 3.0/12.0)) fx
        fx

    let line4 = 
        let fx = wavefun
        let fx = linearDrop 1000.0 fx
        let fx = repeat 2000.0 fx
        let fx = offset 6000.0 fx
        let fx = freezeFreq (440.0 ** (2.0 - 1.0/12.0)) fx
        fx

    let line34 = 
        let fx = sum [ line3; line4 ]
        let fx = linearFreqDrop 9000.0 1.5 fx
        let fx = repeat 10000.00 fx
        fx

    let master = sum [ line1; line2; line34 ]

    [ for i in 0..8000 * 20 ->
        let time = float i
        
        discretize (line2 -1.0 time) ]
    |> save stream 8000
    |> ignore
    0
