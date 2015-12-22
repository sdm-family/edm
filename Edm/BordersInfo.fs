namespace Edm

type BorderStyle =
  | NoBorder
  | DashDotBorder
  | DashDotDotBorder
  | DashBorder
  | DotBorder
  | DoubleLineBorder
  | HairLineBorder
  | MediumBorder
  | MediumDashDotBorder
  | MediumDashDotDotBorder
  | MediumDashBorder
  | ThickBorder
  | ThinBorder
  | SlantedDashDotBorder

type BorderInfo = {
  Style: BorderStyle
  Color: RgbColor
}

type DiagonalBorderInfo = {
  Border: BorderInfo
  TopLeftToBottomRight: bool
  BottomLeftToTopRight: bool
}

type BordersInfo = {
  Top: BorderInfo
  Right: BorderInfo
  Bottom: BorderInfo
  Left: BorderInfo
  Diagonal: DiagonalBorderInfo
}