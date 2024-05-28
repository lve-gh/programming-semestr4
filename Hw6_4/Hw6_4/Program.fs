module Hw6_4

open System

type WorkflowRound(accuracy : int) =
    member this.Bind (x : float, f) = f (Math.Round(x, accuracy))
    member this.Return (x : float) = Math.Round(x, accuracy)

type WorkflowStrings() =
    member this.Bind (x : string, f) =
        match Int32.TryParse x with
        | true, y -> f y
        | false, y -> None
    member this.Return x = Some x
