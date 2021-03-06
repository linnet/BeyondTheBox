﻿open System
open System.Diagnostics
open System.IO

do (* PROGRAM *)
  // path to server executable
  let server  = Path.Combine (__SOURCE_DIRECTORY__
                             ,"chatz.server/chatz.server.exe")
  // path to client executable
  let client  = Path.Combine (__SOURCE_DIRECTORY__
                             ,"chatz.client/bin/Debug/chatz.client.exe")
  // program configuration
  let debug   = match fsi.CommandLineArgs with
                | [| _; "debug" |]  -> true
                | _                 -> false

  // launch server
  if debug then Environment.SetEnvironmentVariable("RUST_LOG","chatz.server=4")
  Process.Start server |> ignore
  // launch two clients, each with a different handle
  [ "pblasucci"; "Rachley" ]
  |> Seq.map  (fun name -> client,name)
  |> Seq.iter (Process.Start  >> ignore)
  // also launch an empty client (which will prompt for a handle)
  Process.Start client |> ignore
