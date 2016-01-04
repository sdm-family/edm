namespace Edm

type FontName =
  | NoFontName
  | FontName of string

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module FontName =
  let toOption = function
  | NoFontName -> None
  | FontName name -> Some name

  let noSpecific = NoFontName
  let create name = FontName name

type FontSize =
  | NoFontSize
  | FontSize of float

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module FontSize =
  let toOption = function
  | NoFontSize -> None
  | FontSize size -> Some size

  let noSpecific = NoFontSize
  let create size = FontSize size

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

  let noSpecific = NoFontStyle
  let standard = StandardStyle
  let bold = BoldStyle
  let itaric = ItaricStyle
  let boldItaric = BoldItaricStyle

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

  let noSpecific = NoDecoration

type FontInfoData = {
  Name: FontName
  Size: FontSize
  Style: FontStyle
  Color: RgbColor
  Underline: Underline option
  Decoration: TextDecoration
}

type FontInfo =
  | NoFontInfo
  | FontInfo of FontInfoData

module Font =
  let noSpecific = NoFontInfo

  let createRaw name size style color underline decoration =
    FontInfo { Name = name; Size = size; Style = style; Color = color; Underline = underline; Decoration = decoration }

  let create name size style color =
    createRaw name size style color None NoDecoration

  let underlined name size style color =
    createRaw name size style color (Some Underline) NoDecoration