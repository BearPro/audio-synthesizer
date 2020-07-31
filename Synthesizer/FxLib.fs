module FxLib

let linearDrop duration (wavefun: float -> float -> float) freq index =
    if index > duration
    then 0.0
    else 
        let coefficient = duration - index / float duration
        coefficient * (wavefun freq index)

let repeat period (wavefun: float -> float -> float) freq index =
    wavefun freq (index % period)

let dildo a b = a + b


let double_dildo a = dildo (a * 2)
