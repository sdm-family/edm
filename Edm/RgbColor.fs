namespace Edm

type RgbColor =
  | NoColor
  | Rgb of Red:int * Green:int * Blue:int
with
  override this.ToString() =
    match this with
    | NoColor -> "NoColor"
    | Rgb (r, g, b) -> sprintf "Rgb (Red=%d, Green=%d, Blue=%d)" r g b

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module RgbColor =
  let create (r, g, b) = Rgb (r, g, b)

  let toOption = function
  | NoColor -> None
  | Rgb (r, g, b) -> Some (r, g, b)