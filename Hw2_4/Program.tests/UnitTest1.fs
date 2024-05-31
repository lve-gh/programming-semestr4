module Program.tests

open NUnit.Framework
open FsUnit
open FsCheck
open Tree
//1
[<Test>]
let MapEqualsFilter () =
    Check.QuickThrowOnFailure(fun l -> evenCountMap l = evenCountFilter l)

[<Test>]
let MapEqualsFold () =
    Check.QuickThrowOnFailure(fun l -> evenCountMap l = evenCountFold l)

[<Test>]
let FoldEqualsFilter () =
    Check.QuickThrowOnFailure(fun l -> evenCountFold l = evenCountFilter l)

//2
[<Test>]
let ``Should return the expected tree``() =
    let inputTree = Node(1, Node(2, Empty, Node(3, Empty, Empty)), Empty)
    let expectedTree = Node(2, Node(4, Empty, Node(6, Empty, Empty)), Empty)
    let actualTree = map (fun x -> x * 2) inputTree
    actualTree |> should equal expectedTree

[<Test>]
let ``Non-degenerate tree``() =
    let inputTree = Node(1, Node(2, Node(3, Empty, Empty), Node(4, Empty, Empty) ), Node(5, Node(6, Empty, Empty), Node(7, Empty, Empty)))
    let expectedTree = Node(2, Node(4, Node(6, Empty, Empty), Node(8, Empty, Empty) ), Node(10, Node(12, Empty, Empty), Node(14, Empty, Empty)))
    let actualTree = map (fun x -> x * 2) inputTree
    actualTree |> should equal expectedTree

//3
[<Test>]
let ``Should return correct result``() =
    let exprTree = Binary(Add, Const(2), Binary(Mul, Const(3), Const(4)))
    let result = eval exprTree
    result |> should equal 14


//4
[<Test>]
let ``Lists should be equal``() =
    let expectedPrimes = [2; 3; 5; 7; 11; 13; 17; 19; 23; 29] 

    let rec isPrime n =
        let upperBound = int(sqrt(float n))
        let rec checkDivisor divisor =
            divisor > upperBound || (n % divisor <> 0 && checkDivisor (divisor + 1))
        checkDivisor 2

    let primes = Seq.unfold (fun state -> Some(state, state + 1)) 2
                |> Seq.filter isPrime

    let actualPrimes = primes |> Seq.take 10 |> Seq.toList
    //let actualPrimes = primes |> Seq.take 10 |> Seq.toList 
    actualPrimes |> should equal expectedPrimes