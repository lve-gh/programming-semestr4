module NetworkSimulationTests

open NUnit.Framework
open NetworkSimulation

let createTestNetwork () =
    let matrix = [| [|false; true; false|]; [|true; false; true|]; [|false; true; false|] |]
    let probabilities = Map.ofList [ OS.Windows, 1.0; OS.Linux, 0.0 ]
    Network(matrix, probabilities)

[<Test>]
let ``Check the simulation`` () =
    let network = createTestNetwork ()
    network.RunSimulation()
    Assert.AreEqual(network.RunSimulation(), network)

[<Test>]
let ``Check the Network`` () =
    let matrix = [| [|false; true; false|]; [|true; false; true|]; [|false; true; false|] |]
    let probabilities = Map.ofList [ OS.Windows, 1.0; OS.Linux, 0.0 ]
    let network = createTestNetwork ()
    Assert.AreEqual(Network(matrix, probabilities), network)