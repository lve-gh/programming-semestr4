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
    let mutable value : 'a option = None
    let gate = obj()
    interface ILazy<'a> with
        member this.Get() =
            match value with
            | Some v -> v
            | None ->
                lock gate (fun () ->
                    match value with
                    | Some v -> v
                    | None ->
                        let v = supplier()
                        value <- Some v
                        v
                )


type LockFreeLazy<'a>(supplier : unit -> 'a) =
    let privateSupplier : unit -> 'a = supplier
    let mutable result : Option<'a> = None
    interface ILazy<'a> with
        member this.Get () =
            match result with
            | None ->
                let value = privateSupplier ()
                Interlocked.CompareExchange(&result, Some value, None) |> ignore
                result.Value
            | Some value -> value


let lazyValue = LockFreeLazy<int>(fun () -> 1 + 2 + 3)

let value = (lazyValue :> ILazy<int>).Get()
printf "%d" value

