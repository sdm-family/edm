namespace Edm

type RichTextSegment = {
  Value: string
  FontInfo: FontInfo
}

type RichText = {
  Segments: RichTextSegment list
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

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Data =
  let editRichText f = function
  | RichText text -> RichText (f text)
  | other -> other

  let editRichNumber f = function
  | RichNumber num -> RichNumber (f num)
  | other -> other

  let editFormula f = function
  | Formula str -> Formula (f str)
  | other -> other

  let editOther f = function
  | Other x -> Other (f x)
  | other -> other