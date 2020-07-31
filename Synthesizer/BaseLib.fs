module BaseLib

type Wave = float -> float -> float

open System

/// Возвращает значение синусоиды указанной частоты в указанной координате.
let wave freq position = sin(position * freq)

/// Преобразует вещественное значение в диапозоне [0; 1] в целочисленное.
let discretize value = 
    int16 (value * float Int16.MaxValue)

let wavefun frequency position =
    wave (Math.PI * 2.0 * frequency / 8000.0) position

let linearFreqDrop duration targetCoefficient (wavefun: Wave) freq position =
    if position > duration
    then wavefun (freq * targetCoefficient) position
    else
        let coefficient = duration - position / float duration
        wavefun (freq * (1.0 - targetCoefficient) * coefficient) position

let freezeFreq freq (wave: Wave) _ position =
    wave freq position

let sum (waves: Wave seq) freq position =
    let coefficient = 1.0 / (waves |> Seq.length |> float)
    Seq.sumBy (fun wave -> coefficient * wave freq position) waves

let offset offset (wave: Wave) freq position =
    if position < offset
    then 0.0
    else wave freq (position - offset)