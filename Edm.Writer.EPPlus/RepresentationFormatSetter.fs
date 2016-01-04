module Edm.Writer.EPPlus.RepresentationFormatSetter

open Edm
open OfficeOpenXml

let nFmtcToStr = function
| NFCLiteral "General" -> "General"
| NFCLiteral lit -> "\"" + lit + "\""
| NFCTextInputSection -> "@"
| NFCSpace baseChar -> "_" + string baseChar
| NFCFill ch -> "*" + string ch
| NFCZero -> "0"
| NFCSharp -> "#"
| NFCQuestion -> "?"
| NFCPeriod -> "."
| NFCSlash -> "/"
| NFCComma -> ","
| NFCPercent -> "%"
| NFCExponent (e, true) -> string e + "+"
| NFCExponent (e, false) -> string e + "-"

let dtFmtcToStr = function
| DTFCLiteral "General" -> "General"
| DTFCLiteral lit -> "\"" + lit + "\""
| DFCM -> "m"
| DFCMm -> "mm"
| DFCMmm -> "mmm"
| DFCMmmm -> "mmmm"
| DFCMmmmm -> "mmmmm"
| DFCD -> "d"
| DFCDd -> "dd"
| DFCDdd -> "ddd"
| DFCDddd -> "dddd"
| DFCYy -> "yy"
| DFCYyyy -> "yyyy"
| TFCH -> "h"
| TFCElapsedH -> "[h]"
| TFCHH -> "hh"
| TFCM -> "m"
| TFCElapsedM -> "[m]"
| TFCMm -> "mm"
| TFCS -> "s"
| TFCElapsedS -> "[s]"
| TFCSS -> "ss"
| TFCAmPm LongLergeAmPm -> "AM/PM"
| TFCAmPm LongSmallAmPm -> "am/pm"
| TFCAmPm ShortLergeAmPm -> "A/P"
| TFCAmPm ShortSmallAmPm -> "a/p"
| TFCMillisec -> ".00"

let fmtToStr = function
| NumericFormat xs -> xs |> List.map nFmtcToStr |> String.concat ""
| DateTimeFormat xs -> xs |> List.map dtFmtcToStr |> String.concat ""

let toStr (codeSec: CodeSection) : string =
  let color = match codeSec.Color with None -> "" | Some color -> "[" + string color + "]"
  let cond =
    match codeSec.Condition with
    | None -> ""
    | Some (cond, value) ->
        match cond with
        | COLt -> "[<" + string value + "]"
        | COLte -> "[<=" + string value + "]"
        | COGt -> "[>" + string value + "]"
        | COGte -> "[>=" + string value + "]"
  color + cond + fmtToStr codeSec.Format

let setTo (format: Style.ExcelNumberFormat) reprFmt =
  format.Format <- match reprFmt with
                   | FullReprFormat (pos, neg, zero, text) -> (toStr pos) + ";" + (toStr neg) + ";" + (toStr zero) + ";" + (toStr text)
                   | PosNegZeroReprFormat (pos, neg, zero) -> (toStr pos) + ";" + (toStr neg) + ";" + (toStr zero)
                   | PosNegReprFormat (zeroAndPos, neg) -> (toStr zeroAndPos) + ";" + (toStr neg)
                   | OneReprFormat all -> toStr all
