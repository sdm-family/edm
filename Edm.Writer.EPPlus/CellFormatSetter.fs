module Edm.Writer.EPPlus.CellFormatSetter

open Edm
open OfficeOpenXml
open System.Drawing

let setBorderTo (border: Style.ExcelBorderItem) (info: BorderInfo) =
  let toEPPlusBorderStyle = function
  | NoBorder -> Style.ExcelBorderStyle.None
  | DashDotBorder -> Style.ExcelBorderStyle.DashDot
  | DashDotDotBorder -> Style.ExcelBorderStyle.DashDotDot
  | DashBorder -> Style.ExcelBorderStyle.Dashed
  | DotBorder -> Style.ExcelBorderStyle.Dotted
  | HairLineBorder -> Style.ExcelBorderStyle.Hair
  | MediumBorder -> Style.ExcelBorderStyle.Medium
  | MediumDashDotBorder -> Style.ExcelBorderStyle.MediumDashDot
  | MediumDashDotDotBorder -> Style.ExcelBorderStyle.MediumDashDotDot
  | MediumDashBorder -> Style.ExcelBorderStyle.MediumDashed
  | ThickBorder -> Style.ExcelBorderStyle.Thick
  | ThinBorder -> Style.ExcelBorderStyle.Thin
  | DoubleLineBorder -> Style.ExcelBorderStyle.Double
  | SlantedDashDotBorder -> failwith "SlantedDashDotBorder is not supported by EPPlus"

  border.Style <- toEPPlusBorderStyle info.Style
  if info.Style <> NoBorder then
    info.Color |> RgbColor.toOption |> Option.iter (fun (r, g, b) -> border.Color.SetColor(Color.FromArgb(r, g, b)))

let setBordersTo (border: Style.Border) (info: BordersInfo) =
  info.Top |> setBorderTo border.Top
  info.Right |> setBorderTo border.Right
  info.Bottom |> setBorderTo border.Bottom
  info.Left |> setBorderTo border.Left
  info.Diagonal.Border |> setBorderTo border.Diagonal
  border.DiagonalDown <- info.Diagonal.TopLeftToBottomRight
  border.DiagonalUp <- info.Diagonal.BottomLeftToTopRight

let setLayoutTo (style: Style.ExcelStyle) (layout: LayoutInfo) =
  let hl, indent =
    match layout.HorizontalLayout with
    | HLStandard -> Style.ExcelHorizontalAlignment.General, None
    | HLLeft indent -> Style.ExcelHorizontalAlignment.Left, Some indent
    | HLCenter -> Style.ExcelHorizontalAlignment.Center, None
    | HLRight indent -> Style.ExcelHorizontalAlignment.Right, Some indent
    | HLJustify -> Style.ExcelHorizontalAlignment.Justify, None
    | HLEqualSpacing (indent, _arroundSpace) -> Style.ExcelHorizontalAlignment.Distributed, Some indent // 「前後にスペースを入れる」はEPPlusでは非対応っぽい
  style.HorizontalAlignment <- hl
  indent |> Option.iter (fun indent -> style.Indent <- indent)

  let vl, tc =
    match layout.VerticalLayout with
    | VLSup tc -> Style.ExcelVerticalAlignment.Top, tc
    | VLCenter tc -> Style.ExcelVerticalAlignment.Center, tc
    | VLSub tc -> Style.ExcelVerticalAlignment.Bottom, tc
    | VLJustify wrap -> Style.ExcelVerticalAlignment.Justify, if wrap then WrapText else NoTextControl
    | VLEqualSpacing wrap -> Style.ExcelVerticalAlignment.Distributed, if wrap then WrapText else NoTextControl
  style.VerticalAlignment <- vl
  let wrapText, shrinkToFit =
    match tc with
    | NoTextControl -> false, false
    | WrapText -> true, false
    | ShrinkToFit -> false, true
  style.WrapText <- wrapText
  style.ShrinkToFit <- shrinkToFit

let setTo (target: ExcelRange) (format: FormatInfo) =
  format.Borders |> setBordersTo target.Style.Border
  format.BackgroundColor |> RgbColor.toOption |> Option.iter (fun (r, g, b) ->
    target.Style.Fill.PatternType <- Style.ExcelFillStyle.Solid
    target.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(r, g, b)))
  format.Layout |> setLayoutTo target.Style
  format.RepresentationFormat |> RepresentationFormatSetter.setTo target.Style.Numberformat