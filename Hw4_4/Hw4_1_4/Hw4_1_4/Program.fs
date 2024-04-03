module Hw4_1_4

open System.Collections.Generic

let isValid (input: string) =
    let mutable isValid = true
    let stack = Stack<char>()
    let isOpening c = c = '{' || c = '[' || c = '('
    let isClosing c = c = '}' || c = ']' || c = ')'
    let isMatchingOpening c closing =
        match closing with
        | '}' -> c = '{'
        | ']' -> c = '['
        | ')' -> c = '('
        | _ -> false

    for c in input do
        if isOpening c then
            stack.Push(c)
        elif isClosing c then
            match stack.TryPeek() with
            | true, top when isMatchingOpening top c -> stack.Pop() |> ignore
            | _ -> isValid <- false

    isValid && stack.Count = 0

let string = "()]"

let a = isValid string

printf "%b" a