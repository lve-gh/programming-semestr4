module Hw3_4

type Term =
    | Var of string
    | Abs of string * Term
    | App of Term * Term

let rec substitute (term: Term) (varName: string) (replacement: Term) =
    match term with
    | Var v -> if v = varName then replacement else Var v
    | Abs (v, t) -> if v = varName then Abs(v, t) else Abs(v, substitute t varName replacement)
    | App (t1, t2) -> App(substitute t1 varName replacement, substitute t2 varName replacement)

let rec betaReduce (term: Term) =
    match term with
    | App (Abs (var, body), arg) -> substitute body var arg
    | App (t1, t2) -> App(betaReduce t1, betaReduce t2)
    | Abs (v, t) -> Abs(v, betaReduce t)
    | _ -> term

let rec alphaConvert (term: Term) (oldName: string) (newName: string) =
    match term with
    | Var v -> if v = oldName then Var newName else Var v
    | Abs (v, t) -> if v = oldName then Abs(newName, alphaConvert t oldName newName) else Abs(v, alphaConvert t oldName newName)
    | App (t1, t2) -> App(alphaConvert t1 oldName newName, alphaConvert t2 oldName newName)

let rec normalize (term: Term) =
    let rec normalizeHelper t =
        let reducedTerm = betaReduce t
        if reducedTerm = t then
            t
        else
            normalizeHelper reducedTerm
    normalizeHelper term

let example =  App(
               Abs("c", App(Var "c", Var "b")),
               Abs("a", Var "a"))

let result = normalize example

match example with
| Var v -> printfn "%s" v
| _ -> printfn "Invalid expression"