module Kata

type LoggingOption = Enabled | Disabled

type PortNumber = int
type PortOption = Disabled | Enabled of PortNumber

type ComandLineArgsInfo = { 
    Logging : LoggingOption;
    Port : PortOption; 
    Directory: Option<string> }

let defaultOptions = {Logging = LoggingOption.Disabled; Port = PortOption.Disabled; Directory = None}

let parseCmdArgs args =
    defaultOptions

    
    