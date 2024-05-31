module NetworkSimulation

open System

type OS =
    | Windows
    | Linux
    | MacOS

type Computer(os: OS, isInfected: bool) =
    member val OS = os
    member val IsInfected = isInfected

let ComputersInNetworks(windows: int, linux: int, macOS: int) =
    let windowsComputers = Array.init windows (fun i -> Computer(OS.Windows, false))
    let linuxComputers = Array.init linux (fun i -> Computer(OS.Linux, false))
    let macOSComputers = Array.init macOS (fun i ->  Computer(OS.MacOS, false))
    Array.concat [windowsComputers ; linuxComputers ; macOSComputers]

type Network(adjacencyMatrix: bool[][], infectionProbabilities: Map<OS, float>, winCount: int, linuxCount: int, macCount: int) =
    let computers = ComputersInNetworks(winCount, linuxCount, macCount) 
    let random = Random()

    let isConnected (computer1: int) (computer2: int) = adjacencyMatrix.[computer1].[computer2]

    let getInfectionProbability (computer: Computer) =
        match infectionProbabilities.TryFind computer.OS with
        | Some(probability) -> probability
        | None -> 0.0

    let infectComputer (computerIndex: int) =
        let computer = computers.[computerIndex]
        let t = getInfectionProbability computer
        if not computer.IsInfected && random.NextDouble() < getInfectionProbability computer then
            let infectedComputer = Computer(computer.OS, true)
            computers.[computerIndex] <- infectedComputer
            //printfn "Computer %d is infected!" computerIndex
            infectedComputer
        else
            computer

    let simulateStep () =
        let infectedComputers = 
            computers 
            |> Array.mapi (fun i (computer: Computer) -> if computer.IsInfected then None else Some i)
        infectedComputers
        |> Array.choose id
        |> Array.filter (fun infectedIndex -> infectedIndex < adjacencyMatrix.Length)
        |> Array.map (fun infectedIndex ->
            adjacencyMatrix[infectedIndex]
            |> Array.mapi (fun j isConnected -> if isConnected then Some j else None)
            |> Array.choose id
            |> Array.map infectComputer)   

    //let printNetworkState () =
    //    printfn "Network state:"
    //    computers |> Array.iteri (fun i computer -> printfn "Computer %d (%A): %s" i computer.OS (if computer.IsInfected then "Infected" else "Healthy"))

    member this.getComputers() =
        computers

    member this.areAllComputersInfected() : bool =
        computers |> Array.forall (fun computer -> computer.IsInfected)

    member this.RunSimulation () =
        while Array.exists (fun (computer: Computer) -> not computer.IsInfected) computers do
            let a = simulateStep ()
            a |> ignore
            //None
            //expr |> ignore 
            //printfn ""
            //printNetworkState ()
            //printfn "-------------------------"


let random1 = Random()


let matrix = [| [|true; true; true; true|]; [|true; true; true; true|]; [|true; true; true; true|]; [|true; true; true; true|] |]
let probabilities = Map.ofList [ OS.Windows, 0.1; OS.Linux, 0.1 ; OS.MacOS, 0.1]
let network = Network(matrix, probabilities, 1, 2, 1)
network.RunSimulation()

let allComputersInfected = network.areAllComputersInfected()
//printfn "Are all computers infected? %b" allComputersInfected