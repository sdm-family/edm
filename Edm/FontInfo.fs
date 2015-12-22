namespace Edm

type FontName =
  | NoFontName
  | FontName of string

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module FontName =
  let toOption = function
  | NoFontName -> None
  | FontName name -> Some name

type FontSize =
  | NoFontSize
  | FontSize of float

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module FontSize =
  let toOption = function
  | NoFontSize -> None
  | FontSize size -> Some size

type FontStyle =
  | NoFontStyle
  | StandardStyle
  | BoldStyle
  | ItaricStyle
  | BoldItaricStyle

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module FontStyle =
  let toOption = function
  | NoFontStyle -> None
  | StandardStyle -> Some (false, false)
  | BoldStyle -> Some (true, false)
  | ItaricStyle -> Some (false, true)
  | BoldItaricStyle -> Some (true, true)

type TextVerticalPos =
  | Baseline
  | Subscript
  | Superscript

type TextDecoration =
  | NoDecoration
  | Decoration of Strike:bool * TextVerticalPos:TextVerticalPos

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module TextDecoration =
  let toOption = function
  | NoDecoration -> None
  | Decoration (strike, textVerticalPos) -> Some (strike, (textVerticalPos = Subscript), (textVerticalPos = Superscript))

type FontInfoData = {
  Name: FontName
  Size: FontSize
  Style: FontStyle
  Underline: Underline option
  Color: RgbColor
  Decoration: TextDecoration
}

type FontInfo =
  | NoFontInfo
  | FontInfo of FontInfoData
