namespace Edm

type Cell = {
  Row: int
  Column: int
  MergedRows: int
  MergedColumns: int
  Format: FormatInfo
  Data: Data
}
