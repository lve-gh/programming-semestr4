module Hw3_4.tests

open NUnit.Framework
open Hw3_4

[<Test>]
let ``Beta reduction test``() =
    let term = App(Abs("a", Var "a"), Var "b")
    let reducedTerm = betaReduce term
    Assert.AreEqual(Var "b", reducedTerm)

[<Test>]
let ``Alpha conversion test``() =
    let term = Abs("a", App(Var "a", Var "b"))
    let convertedTerm = alphaConvert term "a" "c"
    Assert.AreEqual(Abs("c", App(Var "c", Var "b")), convertedTerm)

[<Test>]
let ``Normalize test``() =
    let term = App(Abs("a", Var "a"), Var "b")
    let normalizedTerm = normalize term
    Assert.AreEqual(Var "b", normalizedTerm)

[<Test>]
let ``Substitute replaces variable with replacement`` () =
    let term = Var "x"
    let varName = "x"
    let replacement = Var "y"
    let expected = Var "y"
    let actual = substitute term varName replacement
    Assert.AreEqual(expected, actual)

[<Test>]
let ``Substitute does not replace unrelated variables`` () =
    let term = Var "x"
    let varName = "y"
    let replacement = Var "z"
    let expected = Var "x"
    let actual = substitute term varName replacement
    Assert.AreEqual(expected, actual)

[<Test>]
let ``Substitute replaces variable in application`` () =
    let term = App(Var "x", Var "y")
    let varName = "x"
    let replacement = Var "z"
    let expected = App(Var "z", Var "y")
    let actual = substitute term varName replacement
    Assert.AreEqual(expected, actual)