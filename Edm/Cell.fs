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
  /// セルを生成します。
  let create (row, col) (rows, cols) format data =
    { Row = row
      Column = col
      MergedRows = rows
      MergedColumns = cols
      Format = format
      Data = data }

  /// テキストの格納されたセルを生成します。
  let richText (row, col) (rows, cols) richText =
    create (row, col) (rows, cols) Format.defaultTextFormat (RichText richText)

  /// 数値の格納されたセルを生成します。
  let richNum (row, col) (rows, cols) richNum =
    create (row, col) (rows, cols) Format.defaultNumberFormat (RichNumber richNum)

  let private editFontInfo f data =
    match data with
    | RichText _ -> data |> Data.editRichText (fun txt -> { txt with FontInfo = f txt.FontInfo })
    | RichNumber _ -> data |> Data.editRichNumber (fun num -> { num with FontInfo = f num.FontInfo })
    | Formula str -> Formula str
    | Other x -> RichText (RichText.create ([ { RichTextSegment.Value = string x; FontInfo = NoFontInfo } ], f NoFontInfo))

  /// セル内のテキストのフォント名を指定した値で更新します。
  let updateFontName fontName cell =
    { cell with Data = cell.Data |> editFontInfo (Font.updateName fontName) }

  /// セル内のテキストのフォントサイズを指定した値で更新します。
  let updateFontSize fontSize cell =
    { cell with Data = cell.Data |> editFontInfo (Font.updateSize fontSize) }

  /// セル内のテキストのフォントサイズを取得します。
  let getFontSize (cell: Cell) =
    let getFontSize' = function
    | FontInfo f -> f.Size
    | NoFontInfo -> NoFontSize

    match cell.Data with
    | RichText txt -> getFontSize' txt.FontInfo // TODO : NoFontSizeだった場合に、txtのSegmentsの中まで見て返せそうなら返すようにする？
    | RichNumber num -> getFontSize' num.FontInfo
    | _ -> NoFontSize

  /// セル内のテキストの色を指定した値で更新します。
  /// NoColorを設定しない場合で、RgbColor オブジェクトを持っていない場合、
  /// updateColorRaw の方が簡単に使えます。
  let updateColor color cell =
    { cell with Data = cell.Data |> editFontInfo (Font.updateColor color) }

  /// セル内のテキストの色を指定した値で更新します。
  /// RgbColor オブジェクトを持っている場合や、色情報を持たせないようにしたい場合、
  /// この関数の代わりに updateColor 関数を使ってください。
  let updateColorRaw (r, g, b) cell =
    { cell with Data = cell.Data |> editFontInfo (Font.updateColor (Rgb (r, g, b))) }

  /// セル内のテキストの色を指定した値で更新します。
  /// NoColorを設定しない場合で、RgbColor オブジェクトを持っていない場合、
  /// updateBackgroundColorRaw の方が簡単に使えます。
  let updateBackgroundColor color (cell: Cell) =
    { cell with Format = { cell.Format with BackgroundColor = color } }

  /// セル内のテキストの色を指定した値で更新します。
  /// RgbColor オブジェクトを持っている場合や、色情報を持たせないようにしたい場合、
  /// この関数の代わりに updateBackgroundColor 関数を使ってください。
  let updateBackgourndColorRaw (r, g, b) (cell: Cell) =
    { cell with Format = { cell.Format with BackgroundColor = Rgb (r, g, b) } }

  /// セル内のテキストを太字に設定します。
  let toBold cell =
    let toBold' = function
    | FontInfo f ->
        FontInfo { f with
                     Style =
                       match f.Style with
                       | ItaricStyle | BoldItaricStyle -> BoldItaricStyle
                       | _ -> BoldStyle }
    | NoFontInfo -> Font.create NoFontName NoFontSize BoldStyle NoColor

    { cell with Data = cell.Data |> editFontInfo toBold' }

  /// セル内のテキストにアンダーラインを設定します。
  let withUnderline cell =
    let withUnderline' = function
    | FontInfo f -> FontInfo { f with Underline = Some Underline }
    | NoFontInfo -> Font.underlined NoFontName NoFontSize NoFontStyle NoColor

    { cell with Data = cell.Data |> editFontInfo withUnderline' }

  /// セルの下部分の罫線を指定した値で更新します。
  let updateBottomBorder (style, color) (cell: Cell) =
    let border = { Style = style; Color = color }
    { cell with
        Format = { cell.Format with Borders = { cell.Format.Borders with Bottom = border } } }

  /// セルの上部分の罫線を指定した値で更新します。
  let updateTopBorder (style, color) (cell: Cell) =
    let border = { Style = style; Color = color }
    { cell with
        Format = { cell.Format with Borders = { cell.Format.Borders with Top = border } } }

  /// セルの左部分の罫線を指定した値で更新します。
  let updateLeftBorder (style, color) (cell: Cell) =
    let border = { Style = style; Color = color }
    { cell with
        Format = { cell.Format with Borders = { cell.Format.Borders with Left = border } } }

  /// セルの右部分の罫線を指定した値で更新します。
  let updateRightBorder (style, color) (cell: Cell) =
    let border = { Style = style; Color = color }
    { cell with
        Format = { cell.Format with Borders = { cell.Format.Borders with Right = border } } }

  /// セルの水平レイアウトを指定した値で更新します。
  let updateHorizontalLayout hl (cell: Cell) =
    { cell with Format = { cell.Format with Layout = { cell.Format.Layout with HorizontalLayout = hl } } }

  /// セルの垂直レイアウトを指定した値で更新します。
  /// テキストの折り返し設定のみを更新するには toWrapText が、
  /// 縮小して全体を表示するには toShrinkToFit がこの関数よりも便利に使えます。
  let updateVerticalLayout vl (cell: Cell) =
    { cell with Format = { cell.Format with Layout = { cell.Format.Layout with VerticalLayout = vl } } }

  /// テキストの折り返しを有効にします。
  let toWrapText (cell: Cell) =
    let vl =
      match cell.Format.Layout.VerticalLayout with
      | VLSup _ -> VLSup WrapText
      | VLCenter _ -> VLCenter WrapText
      | VLSub _ -> VLSub WrapText
      | VLJustify _ -> VLJustify true
      | VLEqualSpacing _ -> VLEqualSpacing true
    { cell with Format = { cell.Format with Layout = { cell.Format.Layout with VerticalLayout = vl } } }

  /// 縮小して全体を表示するようにします。
  /// 折り返しの設定とは同時に使えません。
  /// また、レイアウトが VLJustify / VLEqualSpacing の場合は VLCenter に変更された上で設定されます。
  /// これは、VLJustify / VLEqualSpacing が縮小して全体を表示する設定と同時に設定できない制限のためです。
  let toShrinkToFit (cell: Cell) =
    let vl =
      match cell.Format.Layout.VerticalLayout with
      | VLSup _ -> VLSup ShrinkToFit
      | VLCenter _ -> VLCenter ShrinkToFit
      | VLSub _ -> VLSub ShrinkToFit
      | VLJustify _ -> VLCenter ShrinkToFit
      | VLEqualSpacing _ -> VLCenter ShrinkToFit
    { cell with Format = { cell.Format with Layout = { cell.Format.Layout with VerticalLayout = vl } } }