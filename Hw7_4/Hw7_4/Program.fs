module LazyComputation

open System.Threading
open FSharp.Core

type ILazy<'a> =
    abstract member Get: unit -> 'a

type SimpleLazy<'a>(supplier: unit -> 'a) =
    let mutable value = Unchecked.defaultof<'a>
    let mutable evaluated = false
    interface ILazy<'a> with
        member this.Get() =
            if not evaluated then
                value <- supplier()
                evaluated <- true
            value

type ThreadSafeLazy<'a>(supplier: unit -> 'a) =
    let value = Lazy.Create supplier
    interface ILazy<'a> with
        member this.Get() = value.Value

type LockFreeLazy<'a>(supplier : unit -> 'a) =
    let mutable privateSupplier : unit -> 'a = supplier
    let mutable result : Option<'a> = None
    interface ILazy<'a> with
        member this.Get () =
            match result with
            | None ->
                let value = privateSupplier ()
                Interlocked.CompareExchange(&result, Some value, None) |> ignore
                result.Value
            | Some value -> value