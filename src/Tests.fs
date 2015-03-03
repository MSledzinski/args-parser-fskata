module Tests

open Kata
open NUnit.Framework
open FsUnit

[<TestFixture>]   
type ``Support of L parameter`` ()=

    [<Test>]
    member this.``given there is no -L arg logging should be disabled`` () =
        parseCmdArgs [] |> fun x -> x.Logging |> should equal LoggingOption.Disabled

    [<Test>]
    member this.``given there is -L arg logging should be enabled`` () =
        parseCmdArgs [ "-L" ] |> fun x -> x.Logging |> should equal LoggingOption.Enabled

[<TestFixture>]
type ``Support for -P parameter`` () =
    
    [<Test>]
    member this.``given there is no -P arg port option should be disabled`` () =
        parseCmdArgs [] |> fun x -> x.Port |> should equal PortOption.Disabled

    [<Test>]
    member this.``given there is -P 9999 arg port option should be enabled`` () = 
         parseCmdArgs [ "-P"; "9999" ] |> fun x -> x.Port |> should equal (PortOption.Enabled 9999)

    [<Test>]
    member this.``given there is -P ABC arg, parsing should end with exception`` () = 
         (fun () -> parseCmdArgs [ "-P"; "ABC" ] |> ignore) |> should throw typeof<System.Exception>
         
    [<Test>]
    member this.``given there is -P 500 arg, parsing should end with exception`` () = 
         (fun () -> parseCmdArgs [ "-P"; "500" ] |> ignore) |> should throw typeof<System.Exception>

[<TestFixture>]
type ``Support for -D parameter`` () = 
    
    [<Test>]
    member this.``given there is no -D arg there should be no path`` () =
        parseCmdArgs [] |> fun x -> x.Directory |> should equal Option.None

    [<Test>]
    member this.``given there is -D c:\temp\ arg path sould be set properly`` () = 
        parseCmdArgs [ "-D"; @"c:\temp\" ] |> fun x -> x.Directory |> should equal (Option.Some @"c:\temp\")

[<TestFixture>]
type ``Happy path tests`` () = 
    
    [<Test>]
    member this.``given -L -P 1234 -D c:\temp args, should be parsed properly`` () =
        parseCmdArgs [ "-L"; "-P";"1234";"-D";@"c:\temp"] 
        |> should equal { Logging = LoggingOption.Enabled; Port = PortOption.Enabled 1234; Directory = Some @"c:\temp" } 

    [<Test>]
    member this.``given -P 1234 -D c:\temp args, should be parsed properly`` () =
        parseCmdArgs [ "-P";"1234";"-D";@"c:\temp"] 
        |> should equal { Logging = LoggingOption.Disabled; Port = PortOption.Enabled 1234; Directory = Some @"c:\temp" } 

[<TestFixture>]
type ``Not so happy path tests`` () = 
    
    [<Test>]
    member this.``given path like -D -L should yield an error about incorrect dir path`` () = 
        (fun () -> parseCmdArgs [ "-D"; "-L" ] |> ignore) |> should throw typeof<System.Exception>