module Hw7_4.tests

open NUnit.Framework

open FsUnit
open LazyComputation


[<Test>]
    let``CheckSimpleLazy``() =
        let expected = 6
        let lazyValue = SimpleLazy<int>(fun () -> 1 + 2 + 3)
        let value = (lazyValue :> ILazy<int>).Get()
        Assert.AreEqual(value, expected)

[<Test>]
     let``CheckThreadSafeLazy``() =
        let expected = 13
        let lazyValue = ThreadSafeLazy<int>(fun () -> 6 * 2 + 1)
        let value = (lazyValue :> ILazy<int>).Get()
        Assert.AreEqual(value, expected)

[<Test>]
      let``CheckLockFreeLazy``() =
        let expected = 3
        let lazyValue = LockFreeLazy<int>(fun () -> 4 / 2 + 1)
        let value = (lazyValue :> ILazy<int>).Get()
        Assert.AreEqual(value, expected)
