namespace Edm.Writer

open Edm

type IWriter =
  abstract Write: sheets:Sheet list -> unit