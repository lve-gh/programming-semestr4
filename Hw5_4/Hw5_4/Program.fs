module NetworkSimulation

open System

type OS =
    | Windows
    | Linux
    | MacOS

type Computer(os: OS, isInfected: bool) =
    member val OS = os
    member val IsInfected = isInfected

type Network(adjacencyMatrix: bool[][], infectionProbabilities: Map<OS, float>) =
    let computers = Array.init adjacencyMatrix.Length (fun i -> Computer(OS.Windows, false)) 
    let random = Random()

    let isConnected (computer1: int) (computer2: int) = adjacencyMatrix.[computer1].[computer2]

    let getInfectionProbability (computer: Computer) =
        match infectionProbabilities.TryFind computer.OS with
        | Some(probability) -> probability
        | None -> 0.0

    let infectComputer (computerIndex: int) =
        let computer = computers.[computerIndex]
        if not computer.IsInfected && random.NextDouble() < getInfectionProbability computer then
            computers.[computerIndex] <- Computer(computer.OS, true)
            printfn "Computer %d is infected!" computerIndex

    let simulateStep () =
        let infectedComputers = 
            computers 
            |> Array.mapi (fun i (computer: Computer) -> if computer.IsInfected then Some i else None)
        infectedComputers
        |> Array.choose id
        |> Array.iter (fun infectedIndex ->
            adjacencyMatrix.[infectedIndex]
            |> Array.mapi (fun j isConnected -> if isConnected then Some j else None)
            |> Array.choose id
            |> Array.iter infectComputer)

    let printNetworkState () =
        printfn "Network state:"
        computers |> Array.iteri (fun i computer -> printfn "Computer %d (%A): %s" i computer.OS (if computer.IsInfected then "Infected" else "Healthy"))

    member this.RunSimulation () =
        while Array.exists (fun (computer: Computer) -> not computer.IsInfected) computers do
            simulateStep ()
            printNetworkState ()
            printfn "-------------------------"