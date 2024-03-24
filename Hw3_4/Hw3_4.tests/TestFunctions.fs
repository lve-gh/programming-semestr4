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