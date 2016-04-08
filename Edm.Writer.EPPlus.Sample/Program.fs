open Edm
open Edm.Writer.EPPlus

let noBorder = { Style = NoBorder; Color = NoColor }

let format = {
  RepresentationFormat = OneReprFormat { Color = None; Condition = None; Format = NumericFormat [ NFCLiteral "General" ] }
  Layout = { HorizontalLayout = HLLeft 0; VerticalLayout = VLCenter NoTextControl }
  Borders = { Top = noBorder; Right = noBorder; Bottom = noBorder; Left = noBorder; Diagonal = { Border = noBorder; TopLeftToBottomRight = false; BottomLeftToTopRight = false } }
  BackgroundColor = NoColor
}

let font = {
  Name = NoFontName
  Size = NoFontSize
  Style = NoFontStyle
  Underline = None
  Color = NoColor
  Decoration = NoDecoration
}

let richNum (num, font) =
  RichNumber { RichNumber.Value = num; FontInfo = FontInfo font }

let richTxt (txt, font) =
  RichText { RichText.Segments = [ { RichTextSegment.Value = txt; FontInfo = NoFontInfo } ]; FontInfo = FontInfo font }

let titleCols = 15

let nextCells (title: string) (gen: Cell -> Cell) (row: int) =
  let cell = gen { Row = row; Column = titleCols; MergedRows = 1; MergedColumns = 1; Format = format; Data = Other 0 }
  let res = [
    { Row = row; Column = 0; MergedRows = 1; MergedColumns = titleCols; Format = format; Data = Other title }
    cell
  ]
  (res, cell.Row + cell.MergedRows)

let return' x = fun s -> (x, s)
let (>>=) x f =
  fun s -> let (v, s') = x s in (f v) s'

let samples =
  nextCells "1. 値の書き込み" id >>= fun s1 ->
  nextCells "2. 行の結合(2行)" (fun cell -> { cell with MergedRows = 2 }) >>= fun s2 ->
  nextCells "3. 列の結合(4列)" (fun cell -> { cell with MergedColumns = 4 }) >>= fun s3 ->
  nextCells "4. 行と列の結合(3行5列)" (fun cell -> { cell with MergedRows = 3; MergedColumns = 5 }) >>= fun s4 ->
  nextCells "5. フォント名の変更(メイリオ)" (fun cell -> { cell with Data = richNum (10.0, { font with Name = FontName "メイリオ" });
                                                                     MergedColumns = 2;
                                                                     MergedRows = 2 }) >>= fun s5 ->
  nextCells "6. フォントサイズの変更(4)" (fun cell -> { cell with Data = richNum (10.0, { font with Size = FontSize 4.0}) }) >>= fun s6 ->
  nextCells "7. フォントスタイルの変更(Bold)" (fun cell -> { cell with Data = richNum (10.0, { font with Style = BoldStyle });
                                                                       MergedColumns = 2 }) >>= fun s7 ->
  nextCells "8. アンダーライン" (fun cell -> { cell with Data = richNum (10.0, { font with Underline = Some Underline });
                                                         MergedColumns = 2;
                                                         MergedRows = 2 }) >>= fun s8 ->
  nextCells "9. フォント色の変更(青)" (fun cell -> { cell with Data = richNum (1.0, { font with Color = Rgb (0, 0, 255) }) }) >>= fun s9 ->
  nextCells "10. 取り消し線" (fun cell -> { cell with Data = richNum (1.0, { font with Decoration = Decoration (true, Baseline) }) }) >>= fun s10 ->
  nextCells "11. 罫線" (fun cell ->
    let border = { Style = ThinBorder; Color = Rgb (0, 0, 0) }
    let borders =
      { Top = border; Right = border; Bottom = border; Left = border
        Diagonal = { Border = noBorder; TopLeftToBottomRight = false; BottomLeftToTopRight = false } }
    { cell with Format = { format with Borders = borders } }) >>= fun s11 ->
  nextCells "12. 背景色(薄い青)" (fun cell -> { cell with Format = { format with BackgroundColor = Rgb (200, 220, 255) } }) >>= fun s12 ->
  nextCells "13. 改行" (fun cell -> { cell with MergedRows = 2; MergedColumns = 10; Data = richTxt ("hoge\npiyo", font) }) >>= fun s13 ->
  return' [
    yield! s1; yield! s2; yield! s3; yield! s4; yield! s5; yield! s6; yield! s7; yield! s8; yield! s9; yield! s10
    yield! s11; yield! s12; yield! s13
  ]

let noFontRichText text =
  { RichText.Segments = [ { RichTextSegment.Value = text; FontInfo = NoFontInfo } ]; FontInfo = FontInfo font }

let meiryo = { font with Name = FontName "メイリオ" }

let updateFont font text =
  { text with RichText.FontInfo = FontInfo font }

let leftify shapeText =
  { shapeText with HAlign = HALeft }

let sheets = [
  { Name = "sample1"; Cells = samples 0 |> fst; Drawings = [] }
  { Name = "sample2"; Cells = []; Drawings =
      [ Shape.createWithText "hoge" ShapeOfCallout1 (ShapeText.create (noFontRichText "Hello" |> updateFont meiryo)) (TopLeftPixel (10<pixel>, 10<pixel>))
        Drawing.updateSize (Percent 50) (Shape.createWithText "piyo" ShapeOfCallout2 (ShapeText.create (noFontRichText "Hello") |> leftify) (TopLeftPixel (250<pixel>, 10<pixel>)))
        Image.createFromPath "" (System.IO.FileInfo("sample.png")) (TopLeftPixel (10<pixel>, 250<pixel>))] }
]

[<EntryPoint>]
let main _ =
  let writer = EPPlusWriter.create ("output.xlsx", "template.xlsx")
  writer.Write(sheets)
  0