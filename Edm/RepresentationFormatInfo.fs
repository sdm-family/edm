namespace Edm

type RepresentationColor =
  | Black
  | Green
  | White
  | Blue
  | Magenta
  | Yellow
  | Cyan
  | Red

type ConditionOperator =
  | COLt
  | COLte
  | COGt
  | COGte

type NumericFormatChar =
  | NFCLiteral of string
  | NFCTextInputSection
  | NFCSpace of BaseChar:char
  | NFCFill of char
  | NFCZero
  | NFCSharp
  | NFCQuestion
  | NFCPeriod
  | NFCSlash
  | NFCComma
  | NFCPercent
  | NFCExponent of E:char * Plus:bool

type AmPm =
  | LongLergeAmPm
  | LongSmallAmPm
  | ShortLergeAmPm
  | ShortSmallAmPm

type DateTimeFormatChar =
  | DTFCLiteral of string
  | DFCM
  | DFCMm
  | DFCMmm
  | DFCMmmm
  | DFCMmmmm
  | DFCD
  | DFCDd
  | DFCDdd
  | DFCDddd
  | DFCYy
  | DFCYyyy
  | TFCH
  | TFCElapsedH
  | TFCHH
  | TFCM
  | TFCElapsedM
  | TFCMm
  | TFCS
  | TFCElapsedS
  | TFCSS
  | TFCAmPm of AmPm
  | TFCMillisec

type CodeSectionFormat =
  | NumericFormat of NumericFormatChar list
  | DateTimeFormat of DateTimeFormatChar list

type CodeSection = {
  Color: RepresentationColor option
  Condition: (ConditionOperator * float) option
  Format: CodeSectionFormat
}

type RepresentationFormatInfo =
  | FullReprFormat of Positive:CodeSection * Negative:CodeSection * Zero:CodeSection * Text:CodeSection
  | PosNegZeroReprFormat of Positive:CodeSection * Negative:CodeSection * Zero:CodeSection
  | PosNegReprFormat of ZeroAndPositive:CodeSection * Negative:CodeSection
  | OneReprFormat of All:CodeSection

module RepresentationFormat =
  let general =
    OneReprFormat { Color = None; Condition = None; Format = NumericFormat [NFCLiteral "General"] }