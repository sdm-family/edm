namespace Edm

type FormatInfo = {
  RepresentationFormat: RepresentationFormatInfo
  Layout: LayoutInfo
  Borders: BordersInfo
  BackgroundColor: RgbColor
}

module Format =
  let defaultTextFormat =
    { RepresentationFormat = RepresentationFormat.general
      Layout = Layout.defaultTextLayout
      Borders = Borders.noBorders
      BackgroundColor = NoColor }

  let defaultNumberFormat =
    { RepresentationFormat = RepresentationFormat.general
      Layout = Layout.defaultNumberLayout
      Borders = Borders.noBorders
      BackgroundColor = NoColor }