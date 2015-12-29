namespace Edm.Writer.EPPlus

#nowarn "44"

open Edm
open Edm.Writer
open System
open System.IO
open OfficeOpenXml

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
    cell.Data |> DataWriter.writeTo target
    cell.Format |> CellFormatSetter.setTo target

  member this.WriteSheet(sheet: Sheet) =
    let s = this.CurrentBook.Worksheets.Add(sheet.Name, this.TemplateSheet)
    for cell in sheet.Cells do
      this.WriteCell(s, cell)

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