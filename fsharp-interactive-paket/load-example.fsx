
#load "strings.fsx"

open Strings
let name = "your-name" |> StringBuilder.initWith
            |> StringBuilder.append " DJ"
            |> string |> toUpper
printfn "Name: %s" name