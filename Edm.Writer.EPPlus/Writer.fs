﻿namespace Edm.Writer.EPPlus

#nowarn "44"

open Edm
open Edm.Writer
open System
open System.IO
open OfficeOpenXml
open OfficeOpenXml.Drawing

type Writer [<Obsolete("このコンストラクタの代わりにEdm.Writer.EPPlus.EPPlusWriter.createを使ってください。")>]
    (outputFilePath: string, templateFilePath: string) =
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
      this.TemplateSheet <- this.CurrentBook.Worksheets.[1] // EPPlusはすべてが1オリジン

      for sheet in sheets do
        this.WriteSheet(sheet)

      this.CurrentBook.Worksheets.Delete(this.TemplateSheet)
      package.Save()

[<RequireQualifiedAccess>]
module EPPlusWriter =
  let create (outputFilePath: string, templateFilePath: string) =
    Writer(outputFilePath, templateFilePath) :> IWriter