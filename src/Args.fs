module Kata

type LoggingOption = Enabled | Disabled

type PortNumber = int
type PortOption = Disabled | Enabled of PortNumber

type ComandLineArgsInfo = { Logging : LoggingOption; Port : PortOption; Directory: Option<string> }

let minPortNumber = 1025
let (|IsPort|_|) str = 
    match System.Int32.TryParse(str) with
    | (true, int) when int >= minPortNumber-> Some(int)
    | _ -> None

let filePathPattern = @"((?:[a-zA-Z]\:){0,1}(?:[\\][\w.]+){1,}[\\]{0,1})"
let (|IsPath|_|) str = 
  let m = System.Text.RegularExpressions.Regex.Match(str, filePathPattern) 
  if (m.Success) then Some m.Groups.[1].Value else None  

let parseCmdArgs args =
    
    let rec parseCmdArgaInternal args stateOption =
        match args with
        | [] -> stateOption
        | "-L"::tail -> parseCmdArgaInternal tail { stateOption with Logging = LoggingOption.Enabled }
        | "-P"::tail -> 
                        match tail with
                        | IsPort p::xtail-> parseCmdArgaInternal xtail { stateOption with Port = PortOption.Enabled p}
                        | _ -> failwith "Port not specified or is not an integer" 
        | "-D"::tail -> 
                        match tail with
                        | IsPath s::xtail -> parseCmdArgaInternal xtail { stateOption with Directory = Some s}
                        | _ -> failwith "Directory not specified"
        | _ -> failwith "Given input cannot be parsed!"

    parseCmdArgaInternal args { Logging = LoggingOption.Disabled; Port = PortOption.Disabled; Directory = None}