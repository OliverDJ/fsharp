


#load ".paket\\load\\netcoreapp3.1\\Newtonsoft.Json.fsx"

open Newtonsoft.Json

let serialize x = JsonConvert.SerializeObject(x)
let myString = "hei"
let mySerializedString = myString |> serialize