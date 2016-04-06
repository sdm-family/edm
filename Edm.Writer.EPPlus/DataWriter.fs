module Edm.Writer.EPPlus.DataWriter

open Edm
open OfficeOpenXml
open System.Drawing

let mapBoth f (a, b) = (f a, f b)
let getOr defaultValue = function
| None -> defaultValue
| Some value -> value

let mergeOption = function
| None, None -> None
| _, Some y -> Some y // y(後者)を優先する
| Some x, _ -> Some x

let mergeFontInfoData (a: FontInfoData) (b: FontInfoData) =
  let fontName = (a.Name, b.Name) |> mapBoth FontName.toOption |> mergeOption
  let size = (a.Size, b.Size) |> mapBoth FontSize.toOption |> mergeOption
  let style = (a.Style, b.Style) |> mapBoth FontStyle.toOption |> mergeOption
  let underline = (a.Underline, b.Underline) |> mergeOption
  let color = (a.Color, b.Color) |> mapBoth RgbColor.toOption |> mergeOption
  let decoration = (a.Decoration, b.Decoration) |> mapBoth TextDecoration.toOption |> mergeOption

  (fontName, size, style, underline, color, decoration)

let tuplize (f: FontInfoData) =
  (f.Name |> FontName.toOption, f.Size |> FontSize.toOption, f.Style |> FontStyle.toOption, f.Underline, f.Color |> RgbColor.toOption, f.Decoration |> TextDecoration.toOption)

let mergeFontInfo = function
| NoFontInfo, NoFontInfo -> None
| FontInfo baseFontInfo, NoFontInfo -> Some (tuplize baseFontInfo)
| NoFontInfo, FontInfo segFontInfo -> Some (tuplize segFontInfo)
| FontInfo baseFontInfo, FontInfo segFontInfo -> Some (mergeFontInfoData baseFontInfo segFontInfo)

let doNothing = ()

let isMultiline (txt: RichText) =
  txt.Segments |> Seq.exists (fun seg -> seg.Value.Contains("\r") || seg.Value.Contains("\n"))

let addRichTextTo (paragraphCollection: Style.ExcelParagraphCollection) (txt: RichText) =
  let baseFontInfo = txt.FontInfo
  for seg in txt.Segments do
    let paragraph = paragraphCollection.Add(seg.Value)
    match mergeFontInfo (baseFontInfo, seg.FontInfo) with
    | None -> doNothing
    | Some (nameOpt, sizeOpt, styleOpt, underlineOpt, colorOpt, decorationOpt) ->
        nameOpt |> Option.iter (fun name -> paragraph.LatinFont <- name)
        sizeOpt |> Option.iter (fun size -> paragraph.Size <- float32 size)
        styleOpt |> Option.iter (fun (bold, itaric) -> paragraph.Bold <- bold; paragraph.Italic <- itaric)
        underlineOpt |> Option.iter (fun underline ->
          paragraph.UnderLine <-
            match underline with
            | NoUnderline -> Style.eUnderLineType.None
            | Underline -> Style.eUnderLineType.Single
            | DoubleUnderline -> Style.eUnderLineType.Double
            | AccountingUnderline -> Style.eUnderLineType.Single
            | AccountingDoubleUnderline -> Style.eUnderLineType.Double)
        colorOpt |> Option.iter (fun (r, g, b) -> paragraph.Color <- Color.FromArgb(r, g, b))
        decorationOpt |> Option.iter (fun (strike, sub, sup) ->
          paragraph.Strike <- if strike then Style.eStrikeType.Single else Style.eStrikeType.No)

let setRichTextTo (target: ExcelRange) (txt: RichText) =
  if isMultiline txt then
    target.Style.WrapText <- true
  let baseFontInfo = txt.FontInfo
  for seg in txt.Segments do
    let richTxt = target.RichText.Add(seg.Value)
    match mergeFontInfo (baseFontInfo, seg.FontInfo) with
    | None -> doNothing
    | Some (nameOpt, sizeOpt, styleOpt, underlineOpt, colorOpt, decorationOpt) ->
        nameOpt |> Option.iter (fun name -> richTxt.FontName <- name)
        sizeOpt |> Option.iter (fun size -> richTxt.Size <- float32 size)
        styleOpt |> Option.iter (fun (bold, itaric) -> richTxt.Bold <- bold; richTxt.Italic <- itaric)
        underlineOpt |> Option.iter (fun underline -> richTxt.UnderLine <- match underline with NoUnderline -> false | _ -> true)
        colorOpt |> Option.iter (fun (r, g, b) -> richTxt.Color <- Color.FromArgb(r, g, b))
        decorationOpt |> Option.iter (fun (strike, sub, sup) ->
          richTxt.Strike <- strike
          richTxt.VerticalAlign <-
            match sub, sup with
            | true, false -> Style.ExcelVerticalAlignmentFont.Subscript
            | false, true -> Style.ExcelVerticalAlignmentFont.Superscript
            | _ -> Style.ExcelVerticalAlignmentFont.None)

let setRichNumberTo (target: ExcelRange) (num: RichNumber) =
  let toEPPlusUnderLineType = function
  | NoUnderline -> Style.ExcelUnderLineType.None
  | Underline -> Style.ExcelUnderLineType.Single
  | DoubleUnderline -> Style.ExcelUnderLineType.Double
  | AccountingUnderline -> Style.ExcelUnderLineType.SingleAccounting
  | AccountingDoubleUnderline -> Style.ExcelUnderLineType.DoubleAccounting

  target.Value <- num.Value
  match num.FontInfo with
  | NoFontInfo -> doNothing
  | FontInfo fontInfo ->
      let font = target.Style.Font
      fontInfo.Name |> FontName.toOption |> Option.iter (fun name -> font.Name <- name)
      fontInfo.Size |> FontSize.toOption |> Option.iter (fun size -> font.Size <- float32 size)
      fontInfo.Style |> FontStyle.toOption |> Option.iter (fun (bold, itaric) -> font.Bold <- bold; font.Italic <- itaric)
      fontInfo.Underline |> Option.iter (fun underline -> font.UnderLineType <- toEPPlusUnderLineType underline)
      fontInfo.Color |> RgbColor.toOption |> Option.iter (fun (r, g, b) -> font.Color.SetColor(Color.FromArgb(r, g, b)))
      fontInfo.Decoration |> TextDecoration.toOption |> Option.iter (fun (strike, sub, sup) ->
        font.Strike <- strike
        font.VerticalAlign <-
          match sub, sup with
          | true, false -> Style.ExcelVerticalAlignmentFont.Subscript
          | false, true -> Style.ExcelVerticalAlignmentFont.Superscript
          | _ -> Style.ExcelVerticalAlignmentFont.None)

let writeTo (target: ExcelRange) = function
| RichText text -> text |> setRichTextTo target
| RichNumber num -> num |> setRichNumberTo target
| Formula f -> target.Formula <- f
| Other data ->
    let str = string data
    if str.Contains("\r") || str.Contains("\n") then
      target.Style.WrapText <- true
    target.Value <- data