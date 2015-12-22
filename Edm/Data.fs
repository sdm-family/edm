namespace Edm

type RichTextSegment = {
  Value: string
  FontInfo: FontInfo
}

type RichText = {
  Values: RichTextSegment list
  FontInfo: FontInfo
}

type RichNumber = {
  Value: float
  FontInfo: FontInfo
}

type Data =
  | RichText of RichText
  | RichNumber of RichNumber
  | Formula of string
  | Other of obj

