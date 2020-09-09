

#load ".paket\\load\\netcoreapp3.1\\FSharp.Data.fsx"
#load ".paket\\load\\netcoreapp3.1\\Newtonsoft.Json.fsx"

open FSharp.Data
open Newtonsoft.Json
open FSharp.Data.HttpRequestHeaders

let serialize x = JsonConvert.SerializeObject(x)

type PostBody =
    {
        Message: string
    }

let createBody (model: 'a) =
    model 
    |> serialize
    |> FSharp.Data.HttpRequestBody.TextRequest


let postRequest url body = 
    FSharp.Data.Http.Request
                    (   
                        url,
                        headers = 
                            [ 
                                ContentType FSharp.Data.HttpContentTypes.Json
                                // Authorization ("Bearer " + token.AccessToken)
                            ],
                        httpMethod= FSharp.Data.HttpMethod.Post,
                        body = body
                        
                    )
let myUrl = "https://some-server/api/some-endpoint"
let myPost = { Message = "MyMessageToPost" }
let res = 
    myPost 
    |> createBody
    |> postRequest myUrl