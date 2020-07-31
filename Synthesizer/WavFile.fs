module WavFile

open System
open System.IO

let openStream path = 
    if File.Exists path 
        then do File.Delete path
    File.Create path

let save stream (sampleRate: int) (sequence: int16 seq) =
    let writer = new BinaryWriter(stream);
    let frameSize = 16 / 8      // Количество байт в блоке (16 бит делим на 8).
    let length = Seq.length sequence
    let headers = 
        {| RIFF = 0x46464952
           WAVE = 0x45564157
           frm  = 0x20746D66
           DATA = 0x61746164 |}
    writer.Write (int32 <| headers.RIFF)        // Заголовок "RIFF".
    writer.Write (int32 <| 36 + length * frameSize) // Размер файла от данной точки.
    writer.Write (int32 <| headers.WAVE)        // Заголовок "WAVE".
    writer.Write (int32 <| headers.frm)         // Заголовок "frm ".
    writer.Write (int32 <| 16)                  // Размер блока формата.
    writer.Write (int16 <| 1)                   // Формат 1 значит PCM.
    writer.Write (int16 <| 1)                   // Количество дорожек.
    writer.Write (int32 <| sampleRate)          // Частота дискретизации.
    writer.Write (int32 <| sampleRate * frameSize) // Байтрейт (Как битрейт только в байтах).
    writer.Write (int16 <| frameSize)           // Количество байт в блоке.
    writer.Write (int16 <| 16)                  // разрядность.
    writer.Write (int32 <| headers.DATA);       // Заголовок "DATA".
    writer.Write (int32 <| length * frameSize); // Размер данных в байтах.
    for value in sequence do
        for byte in BitConverter.GetBytes value 
            do stream.WriteByte byte
