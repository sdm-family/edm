namespace Edm

type FontName =
  | NoFontName
  | FontName of string

type FontSize =
  | NoFontSize
  | FontSize of float

type FontStyle =
  | NoFontStyle
  | StandardStyle
  | BoldStyle
  | BoldItaricStyle

type TextVerticalPos =
  | Baseline
  | Subscript
  | Superscript

type TextDecoration =
  | NoDecoration
  | Decoration of Strike:bool * TextVerticalPos:TextVerticalPos

type FontInfoData = {
  Name: FontName
  Size: FontSize
  Style: FontStyle
  Underline: Underline
  Color: RgbColor
  Decoration: TextDecoration
}

type FontInfo =
  | NoFontInfo
  | FontInfo of FontInfoData
