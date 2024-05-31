module NetworkSimulationTests

open NUnit.Framework
open NetworkSimulation

[<Test>]
let ``Check all infected`` () =
    let matrix = [| [|true; true; true; true|]; [|true; true; true; true|]; [|true; true; true; true|]; [|true; true; true; true|] |]
    let network = Network(matrix, 1, 2, 1, 1.0, 1.0, 1.0)
    network.RunSimulation()
    let convertToBool (f : unit -> bool) : bool = f ()
    let areAllInfected = convertToBool network.areAllComputersInfected

    Assert.AreEqual(areAllInfected , true)

[<Test>]
let ``Check all is not infected`` () =
    let matrix = [| [|true; true; true; true|]; [|true; true; true; true|]; [|true; true; true; true|]; [|true; true; true; true|] |]
    let network = Network(matrix, 1, 2, 1, 0.0, 0.0, 0.0)
    network.RunSimulation()
    let convertToBool (f : unit -> bool) : bool = f ()
    let areAllInfected = convertToBool network.areAllComputersInfected
    Assert.AreEqual(areAllInfected , false)
