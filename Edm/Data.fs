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
module RichText =
  let create (segments, font) = { Segments = segments; FontInfo = font }
  let createWithoutFontInfo segments = { Segments = segments; FontInfo = NoFontInfo }

  let edit f = function
  | RichText text -> RichText (f text)
  | other -> other

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module RichNumber =
  let create (value, font) = { Value = value; FontInfo = font }
  let createWithoutFontInfo value = { Value = value; FontInfo = NoFontInfo }

  let edit f = function
  | RichNumber num -> RichNumber (f num)
  | other -> other

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Formula =
  let create formula = Formula formula

  let edit f = function
  | Formula str -> Formula (f str)
  | other -> other

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Other =
  let create x = Other x

  let edit f = function
  | Other x -> Other (f x)
  | other -> other

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