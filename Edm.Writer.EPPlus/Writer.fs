namespace Edm.Writer.EPPlus

#nowarn "44"

open Edm
open Edm.Writer
open System
open System.IO
open OfficeOpenXml
open OfficeOpenXml.Drawing

type LongEdge = LEHeight | LEWidth

type PrintArea = {
  StartRow: int
  StartColumn: int
  EndRow: int
  EndColumn: int
}

type Fit = FitToPage | FitToWidth of int | FitToHeight of int

type BookSettings = {
  ShowHorizontalScrollBar: bool option
  ShowVerticalScrollBar: bool option
  ShowSheetTabs: bool option
}

type SheetSettings = {
  ShowGuideLines: bool option
  ShowHeaders: bool option
  LongEdge: LongEdge option
  PrintArea: PrintArea option
  Fit: Fit option
}

type Writer [<Obsolete("このコンストラクタの代わりにEdm.Writer.EPPlus.EPPlusWriterモジュールの関数を使ってください。")>]
    (outputFilePath: string, templateFilePath: string, bookSettings: BookSettings, sheetSettings: SheetSettings) =
  let getCell (range: ExcelRange) { Row = row; Column = col; MergedRows = height; MergedColumns = width } =
    // EPPlusは1オリジンだが、Edmは0オリジンなのでここで調整する
    if height > 1 || width > 1 then
      let merged = range.[row + 1, col + 1, row + height, col + width]
      merged.Merge <- true
      merged
    else
      range.[row + 1, col + 1]

  member val private CurrentBook = Unchecked.defaultof<ExcelWorkbook> with get, set
  member val private TemplateSheet = Unchecked.defaultof<ExcelWorksheet> with get, set

  member __.WriteCell(sheet: ExcelWorksheet, cell: Cell) =
    let target = getCell sheet.Cells cell
    cell.Format |> CellFormatSetter.setTo target
    cell.Data |> DataWriter.writeTo target

  member __.WriteDrawing(sheet: ExcelWorksheet, d: Drawing) =
    let toTextAnchoring = function
    | VABottom -> eTextAnchoringType.Bottom
    | VACenter -> eTextAnchoringType.Center
    | VADistributed -> eTextAnchoringType.Distributed
    | VAJustify -> eTextAnchoringType.Justify
    | VATop -> eTextAnchoringType.Top

    let toTextAlignment = function
    | HALeft -> eTextAlignment.Left
    | HACenter -> eTextAlignment.Center
    | HARight -> eTextAlignment.Right
    | HADistributed -> eTextAlignment.Distributed
    | HAJustified -> eTextAlignment.Justified
    | HAJustifiedLow -> eTextAlignment.JustifiedLow
    | HAThaiDistributed -> eTextAlignment.ThaiDistributed

    let name = d.Name
    let drawing =
      match d.Body with
      | Image (ImageObject image) -> sheet.Drawings.AddPicture(name, image) :> ExcelDrawing
      | Image (ImagePath path) -> sheet.Drawings.AddPicture(name, path) :> ExcelDrawing
      | Shape body ->
          let shape = sheet.Drawings.AddShape(name, body.Type |> Shape.toEPPlusShapeStyle)
          match body.Text with
          | None -> ()
          | Some text ->
              DataWriter.addRichTextTo shape.RichText text.Text
              shape.TextAnchoring <- toTextAnchoring text.VAlign
              shape.TextAlignment <- toTextAlignment text.HAlign
          shape :> ExcelDrawing

    match d.Position with
    | TopLeftPixel (top, left) -> drawing.SetPosition(int top, int left)
    | RowColAndOffsetPixcel (row, col) -> drawing.SetPosition(row.Address, int row.Offset, col.Address, int col.Offset)

    match d.Size with
    | Percent p -> drawing.SetSize(p)
    | Pixel (w, h) -> drawing.SetSize(int w, int h)

  member this.WriteSheet(sheet: Sheet) =
    let s = this.CurrentBook.Worksheets.Add(sheet.Name, this.TemplateSheet)
    sheetSettings.ShowGuideLines |> Option.iter (fun x -> s.View.ShowGridLines <- x)
    sheetSettings.ShowHeaders |> Option.iter (fun x -> s.View.ShowHeaders <- x)
    sheetSettings.LongEdge
    |> Option.iter (function
                    | LEHeight -> s.PrinterSettings.Orientation <- eOrientation.Portrait
                    | LEWidth -> s.PrinterSettings.Orientation <- eOrientation.Landscape)
    sheetSettings.PrintArea
    |> Option.iter (fun a ->
         let range = s.Cells.[a.StartRow + 1, a.StartColumn + 1, a.EndRow + 1, a.EndColumn + 1]
         s.PrinterSettings.PrintArea <- range)
    sheetSettings.Fit
    |> Option.iter (function
                    | FitToPage -> s.PrinterSettings.FitToPage <- true
                    | FitToWidth x -> s.PrinterSettings.FitToWidth <- x
                    | FitToHeight x -> s.PrinterSettings.FitToHeight <- x)
    for cell in sheet.Cells do
      this.WriteCell(s, cell)
    for d in sheet.Drawings do
      this.WriteDrawing(s, d)

  interface IWriter with
    member this.Write(sheets) =
      let outputFile = FileInfo(outputFilePath)
      let templateFile = FileInfo(templateFilePath)

      use package = new ExcelPackage(outputFile, templateFile)
      this.CurrentBook <- package.Workbook
      bookSettings.ShowHorizontalScrollBar
      |> Option.iter (fun x -> this.CurrentBook.View.ShowHorizontalScrollBar <- x)
      bookSettings.ShowVerticalScrollBar
      |> Option.iter (fun x -> this.CurrentBook.View.ShowVerticalScrollBar <- x)
      bookSettings.ShowSheetTabs
      |> Option.iter (fun x -> this.CurrentBook.View.ShowSheetTabs <- x)
      this.TemplateSheet <- this.CurrentBook.Worksheets.[1] // EPPlusはすべてが1オリジン

      for sheet in sheets do
        this.WriteSheet(sheet)

      this.CurrentBook.Worksheets.Delete(this.TemplateSheet)
      package.Save()

[<RequireQualifiedAccess>]
module EPPlusWriter =
  let create (outputFilePath: string, templateFilePath: string) =
    Writer(
      outputFilePath,
      templateFilePath,
      { ShowSheetTabs = None; ShowHorizontalScrollBar = None; ShowVerticalScrollBar = None },
      { ShowGuideLines = None; ShowHeaders = None; LongEdge = None; PrintArea = None; Fit = None }) :> IWriter

  let createWithSettings (bookSettings, sheetSettings) (outputFilePath: string, templateFilePath: string) =
    Writer(outputFilePath, templateFilePath, bookSettings, sheetSettings) :> IWriter

  let createWithBookSettings settings (outputFilePath: string, templateFilePath: string) =
    Writer(
      outputFilePath,
      templateFilePath,
      settings,
      { ShowGuideLines = None; ShowHeaders = None; LongEdge = None; PrintArea = None; Fit = None }) :> IWriter

  let createWithSheetSettings settings (outputFilePath: string, templateFilePath: string) =
    Writer(
      outputFilePath,
      templateFilePath,
      { ShowSheetTabs = None; ShowHorizontalScrollBar = None; ShowVerticalScrollBar = None },
      settings) :> IWriter
