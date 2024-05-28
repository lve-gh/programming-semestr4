module Hw4_1_4

open System.Collections.Generic

let isValid (sequence: string) =
    let openingBrackets = ['{'; '['; '(']
    let closingBrackets = ['}'; ']'; ')']
    

    let rec checkStack stack remaining =
        match remaining with
        | [] -> List.isEmpty stack
        | char :: tail ->
            if List.contains char openingBrackets then
                checkStack (char :: stack) tail
            elif List.contains char closingBrackets then
                match stack with
                | [] -> false
                | openingBracket :: stackTail ->
                    if List.findIndex ((=) openingBracket) openingBrackets = List.findIndex ((=) char) closingBrackets then
                        checkStack stackTail tail
                    else
                        false
            else
                checkStack stack tail

    match sequence with 
    | "" -> false
    | _ -> checkStack [] (List.ofSeq sequence)

//printfn "%b" (checkBrackets "{}")  