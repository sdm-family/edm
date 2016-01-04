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

module Border =
  let noBorder = { Style = NoBorder; Color = NoColor }
  let thinBorder = { Style = ThinBorder; Color = RgbColor.black }
  let thickBorder = { Style = ThickBorder; Color = RgbColor.black }

type DiagonalBorderInfo = {
  Border: BorderInfo
  TopLeftToBottomRight: bool
  BottomLeftToTopRight: bool
}

module DiagonalBorder =
  open Border

  let noDiagonalBorder = { Border = noBorder; TopLeftToBottomRight = false; BottomLeftToTopRight = false }

type BordersInfo = {
  Top: BorderInfo
  Right: BorderInfo
  Bottom: BorderInfo
  Left: BorderInfo
  Diagonal: DiagonalBorderInfo
}

module Borders =
  open Border
  open DiagonalBorder

  let noBorders = { Top = noBorder; Right = noBorder; Bottom = noBorder; Left = noBorder; Diagonal = noDiagonalBorder }
  let arroundThinBorders = { Top = thinBorder; Right = thinBorder; Bottom = thinBorder; Left = thinBorder; Diagonal = noDiagonalBorder }
  let arroundThickBorders = { Top = thickBorder; Right = thickBorder; Bottom = thickBorder; Left = thickBorder; Diagonal = noDiagonalBorder }