module NetworkSimulation

open System

type OS =
    | Windows
    | Linux
    | MacOS

type Computer(os: OS, isInfected: bool, infectionChance: float) =
    member val OS = os
    member val IsInfected = isInfected

let ComputersInNetworks(windows: int, linux: int, macOS: int, winInfectionChance: float, linuxInfectionChance: float, macInfectionChance: float) =
    let windowsComputers = Array.init windows (fun i -> Computer(OS.Windows, false, winInfectionChance))
    let linuxComputers = Array.init linux (fun i -> Computer(OS.Linux, false, linuxInfectionChance))
    let macOSComputers = Array.init macOS (fun i ->  Computer(OS.MacOS, false, macInfectionChance))
    Array.concat [windowsComputers ; linuxComputers ; macOSComputers]

type Network(adjacencyMatrix: bool[][], winCount: int, linuxCount: int, macCount: int, winInfectionChance: float, linuxInfectionChance: float, macInfectionChance: float) =
    let computers = ComputersInNetworks(winCount, linuxCount, macCount, winInfectionChance, linuxInfectionChance, macInfectionChance) 
    let random = Random()

    let isConnected (computer1: int) (computer2: int) = adjacencyMatrix.[computer1].[computer2]

    let getInfectionProbability (computer: Computer) =
        match computer.OS with
        | Windows -> winInfectionChance
        | Linux -> linuxInfectionChance
        | MacOS -> macInfectionChance

    let infectComputer (computerIndex: int) =
        let computer = computers.[computerIndex]
        let t = getInfectionProbability computer
        if not computer.IsInfected && random.NextDouble() < getInfectionProbability computer then
            let infectedComputer = 
                match computer.OS with
                | _ -> Computer(computer.OS, true, computer |> getInfectionProbability)
            computers.[computerIndex] <- infectedComputer
            printfn "Computer %d is infected!" computerIndex
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

    let printNetworkState () =
        printfn "Network state:"
        computers |> Array.iteri (fun i computer -> printfn "Computer %d (%A): %s" i computer.OS (if computer.IsInfected then "Infected" else "Healthy"))

    let IsConnectionExists(i: int) =
        adjacencyMatrix[i] |> Array.forall (fun computer -> true)

    member this.getComputers() =
        computers

    member this.areAllComputersInfected() : bool =
         computers |> Array.forall (fun computer -> computer.IsInfected)


    member this.RunSimulation () =
        let sumOfChances = winInfectionChance + linuxInfectionChance + macInfectionChance
        while Array.exists (fun (computer: Computer) -> not computer.IsInfected && not (sumOfChances <= 0.0)) computers do
            let a = simulateStep ()
            printfn ""
            printNetworkState ()
            printfn "-------------------------"


let random1 = Random()


let matrix = [| [|true; true; true; true|]; [|true; true; true; true|]; [|true; true; true; true|]; [|true; true; true; true|] |]
let network = Network(matrix, 2, 1, 1, 0.5, 0.5, 0.5)
network.RunSimulation()

let allComputersInfected = network.areAllComputersInfected()
//printfn "Are all computers infected? %b" allComputersInfected