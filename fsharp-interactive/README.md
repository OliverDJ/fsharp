
## Starting Fsharp Interactive session in Visual Studios Code.

[logo]: ./images/fsharp-interactive-vs-code.png "Fsharp-interactive-vs-code"
![alt text][logo]

## Including other fsx files
You can load other fsx files into a script. If we have `strings.fsx` containing the following code:
```fsharp
// strings.fsx
let toUpper (s:string) = s.ToUpper()
let toLower (s:string) = s.ToLower()
let replace (oldValue:string) (newValue:string) (s:string) = s.Replace(oldValue,newValue)

module StringBuilder =
    open System.Text
    let init() = new StringBuilder()
    let initWith(s:string) = new StringBuilder(s)
    let append (s:string) (sb:StringBuilder) = sb.Append(s)
```

We can now use it in our  script file like so:

```fsharp
#load "strings.fsx"

open Strings
let name = "Devon" |> StringBuilder.initWith
            |> StringBuilder.append " Burriss"
            |> string |> toUpper
printfn "Name: %s" name
```
We use #load "path/to/script.fsx" to make it available and then open NameOfFileWithoutExtension to import it. So each script file is then treated as a module


## Referencing DLLs
You can reference DLLs using #r "path/to/file.dll". However, Dll's are often stored in a catalogue far away, and may not be easily accessible. We will therefore install paket, a package dependency manager to help us get the nuget packages we require in our fsx file.


## Install Paket dependency manager
You can install paket in a specific directory.

First install a new tool manifest to keep track of your tools.
```
dotnet new tool-manifest
```
This should create `.config\dotnet-tools.json`,

Then install paket:
```
dotnet tool install Paket
```

Initialize paket:
```
dotnet paket init
```

this should create a `paket.dependecies` file and the `paket-files` folder.


## Installing dependencies

To install a dependecy, simply use the the `dotnet paket add` command:

```
dotnet paket add Newtonsoft.Json
```

The paket.dependecies file should be altered fro m its default:
```
source https://api.nuget.org/v3/index.json

storage: none
framework: netcoreapp3.1, netstandard2.0, netstandard2.1
```
to 
```
source https://api.nuget.org/v3/index.json

storage: none
framework: netcoreapp3.1, netstandard2.0, netstandard2.1
nuget Newtonsoft.Json
```

## Using dependencies in a fsx script. 
The reason we have gone through the trouble of installing paket, is to easier get access to the dependencies we want to use in our fsx files.

Now that we have installed paket and succesfully added a dependecy to `paket.dependecies`, its time to use the dependency.

The paket-cli has a powerfull command `generate-load-scripts`. The command generates .fsx files for all dependecies in `paket.dependecies` and stores them in `.paket\load\`, which is convenient and easily accessible. 

```
dotnet paket generate-load-scripts
```
The command should create the folders `.paket\load\netcoreapp3.1\`, `.paket\load\netstandard2.0\` and `.paket\load\netstandard2.1\`. *Note: these three folders are the ones generated because they are the three frameworks added (by default) in the paket.dependencies file.*

Since we added Newtonsoft as a dependency, a file called `Newtonsoft.Json.fsx` should have been generated in each folder.

By loading `Newtonsoft.Json.fsx` into the main fsx file, we have access to the Newtonsoft.Json module. 

``` fsharp
// newtonsoft-example.fsx
#load ".paket\\load\\netcoreapp3.1\\Newtonsoft.Json.fsx"

open Newtonsoft.Json

let serialize x = JsonConvert.SerializeObject(x)
let myString = "hei"
let mySerializedString = myString |> serialize
```





