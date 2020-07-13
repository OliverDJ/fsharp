
#load "strings.fsx"

open Strings
let name = "Oliver" |> StringBuilder.initWith
            |> StringBuilder.append " DJ"
            |> string |> toUpper
printfn "Name: %s" name