
#r "nuget: FSharp.Data, 4.2.7"
#r "nuget: Newtonsoft.Json, 13.0.1"

open Newtonsoft.Json
open FSharp.Data
open FSharp.Data.HttpRequestHeaders

type AccesToken =
    {
        access_token : string
    }
let getAccessToken authUrl clientId clientSecret =
    Http.Request(
        authUrl,
        headers = 
            [ ContentType "application/x-www-form-urlencoded" ],
        query = 
            [  "grant_type"
                , "client_credentials"; "client_id"
                , clientId; "client_secret", clientSecret
            ],
        httpMethod = HttpMethod.Post
    )

let grabTokenFromHttpRespons resp =
    resp.Body
    |> function 
        | Text b -> 
            b 
            |> JsonConvert.DeserializeObject<AccesToken> 
            |> fun t -> t.access_token
        | _ -> "Response body was not a text"

let autUrl = "<auth_url>"// e.g."https://accounts.spotify.com/api/token"
let clientId = "<client_id>"
let clientSecret = "<client_secret>"


let accesToken = 
    (clientId, clientSecret)
    ||> getAccessToken autUrl
    |> grabTokenFromHttpRespons

printfn "Your access token: %s" accesToken
