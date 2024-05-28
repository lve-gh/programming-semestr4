module Hw4_2_4.tests

open NUnit.Framework
open FsCheck

[<Test>]
    let testFuncCorrectness() =
        Check.Quick (fun (x: int) (list: int list) ->
            let expected = func x list 
            let actual = funcPF x list
            Assert.AreEqual(expected, actual);
        )