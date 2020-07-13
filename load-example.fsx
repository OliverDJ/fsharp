
#load "strings.fsx"

open Strings
let name = "Devon" |> StringBuilder.initWith
            |> StringBuilder.append " Burriss"
            |> string |> toUpper
printfn "Name: %s" name