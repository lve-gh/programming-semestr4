module Hw7_4.tests

open NUnit.Framework

open FsUnit
open LazyComputation
open Microsoft.FSharp.Collections
open System.Threading

//type LazyComputationTests() =
//    let supplier1() = 42
//    let supplier2() = "Hello, World!"

//[<Test>]
//    let``Check1``() =
//        let supplier1() = 42
//        let supplier2() = "Hello, World!"
//        let lazyValue = ThreadSafeLazy<int>(supplier1)
//        Assert.AreEqual(42, lazyValue.Get())
//        Assert.AreEqual(42, lazyValue.Get())

//[<Test>]
//    let``Check2``() =
//        let supplier1() = 42
//        let supplier2() = "Hello, World!"
//        let lazyValue = LockFreeLazy<int>(supplier1)
//        Assert.AreEqual(42, lazyValue.Get())

//let testLazy (lazyVar: ILazy<'a>) (expected: 'a) =
//    let actual = lazyVar.Get()
//    Assert.AreEqual(expected, actual)

//[<Test>]
//let testSimpleLazy() =
//    let counter = ref 0
//    let lazyValue = new SimpleLazy<int>(fun () -> incr counter; !counter)
//    testLazy lazyValue 1
//    testLazy lazyValue 1

//[<Test>]
//let testThreadSafeLazy() =
//    let counter = ref 0
//    let lazyValue = new ThreadSafeLazy<int>(fun () -> incr counter; !counter)
//    let threads = [| for _ in 1 .. 10 -> new Thread(fun () -> testLazy lazyValue 1) |]
//    threads |> Array.iter (fun t -> t.Start())
//    threads |> Array.iter (fun t -> t.Join())

//[<Test>]
//let testLockFreeLazy() =
//    let counter = ref 0
//    let lazyValue = new LockFreeLazy<int>(fun () -> incr counter; !counter)
//    let threads = [| for _ in 1 .. 10 -> new Thread(fun () -> testLazy lazyValue 5) |]
//    threads |> Array.iter (fun t -> t.Start())
//    threads |> Array.iter (fun t -> t.Join())

