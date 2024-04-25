module Hw6_4.tests

open NUnit.Framework
open Hw6_4
open FsUnit

[<Test>]
let ``Round the number`` () =
    let rounding = WorkflowRound
    let result = rounding 3 {
        let! a = 2.0 / 12.0
        let! b = 3.5
        return a / b 
    }
    result |> should equal 0.048


[<Test>]
let ``Calculate numbers`` () =
    let calculate = WorkflowStrings()
    let result = calculate {
        let! x = "1"
        let! y = "2"
        let z = x + y
        return z
    }
    result |> should equal (Some 3)

[<Test>]
let ``Calculate strings`` () =
    let calculate = WorkflowStrings()
    let result = calculate {
        let! x = "1"
        let! y = "Ú"
        let z = x + y
        return z
    }
    result |> should equal None