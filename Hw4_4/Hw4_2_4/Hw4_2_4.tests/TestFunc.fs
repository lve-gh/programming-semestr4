module Hw4_2_4.tests

open NUnit.Framework
open FsCheck

[<Test>]
    let testFuncCorrectness() =
        Check.Quick (fun (x: int) (list: int list) ->
            let expected = List.map (fun y -> y * x) list
            let actual = func x list
            expected = actual
        )