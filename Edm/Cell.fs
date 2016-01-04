namespace Edm

type Cell = {
  Row: int
  Column: int
  MergedRows: int
  MergedColumns: int
  Format: FormatInfo
  Data: Data
}

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Cell =
  let create (row, col) (rows, cols) format data =
    { Row = row
      Column = col
      MergedRows = rows
      MergedColumns = cols
      Format = format
      Data = data }

  let richText (row, col) (rows, cols) richText =
    create (row, col) (rows, cols) Format.defaultTextFormat (RichText richText)

  let richNum (row, col) (rows, cols) richNum =
    create (row, col) (rows, cols) Format.defaultNumberFormat (RichNumber richNum)