module NetworkSimulationTests

open NUnit.Framework
open NetworkSimulation

//let createTestNetwork () =
//    let matrix = [| [|true; true; true|]; [|true; true; true|]; [|true; true; true|] |]
//    let probabilities = Map.ofList [ OS.Windows, 1.0; OS.Linux, 0.0 ]
//    Network(matrix, probabilities)

[<Test>]
let ``Check the simulation`` () =
    let matrix = [| [|true; true; true; true|]; [|true; true; true; true|]; [|true; true; true; true|]; [|true; true; true; true|] |]
    let probabilities = Map.ofList [ OS.Windows, 1.0; OS.Linux, 1.0 ; OS.MacOS, 1.0]
    let network = Network(matrix, probabilities, 1, 2, 1)
    network.RunSimulation()
    let areAllInfected = network.areAllComputersInfected
    Assert.AreEqual(areAllInfected , true)


//[<Test>]
//let ``Check the Network`` () =
//    let matrix = [| [|false; true; false|]; [|true; false; true|]; [|false; true; false|] |]
//    let probabilities = Map.ofList [ OS.Windows, 1.0; OS.Linux, 0.0 ]
//    let network = createTestNetwork ()
//    Assert.AreEqual(Network(matrix, probabilities), network)