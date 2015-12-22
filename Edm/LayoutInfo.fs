namespace Edm

type HorizontalLayoutInfo =
  | HLStandard
  | HLLeft of Indent:int
  | HLCenter
  | HLRight of Indent:int
  // | HLFill (* not supported *)
  | HLJustify
  // | HLCenterSelection (* not supported *)
  | HLEqualSpacing of Indent:int * ArroundSpace:bool

type TextControl =
  | NoTextControl
  | WrapText
  | ShrinkToFit

type VerticalLayoutInfo =
  | VLSup of TextControl
  | VLCenter of TextControl
  | VLSub of TextControl
  | VLJustify of Wrap:bool
  | VLEqualSpacing of Wrap:bool

type LayoutInfo = {
  HorizontalLayout: HorizontalLayoutInfo
  VerticalLayout: VerticalLayoutInfo
}