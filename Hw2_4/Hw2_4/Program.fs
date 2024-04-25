// 1
let evenCountMap list =
    list 
    |> List.map (fun x -> if x % 2 = 0 then 1 else 0)
    |> List.sum

let evenCountFilter list =
    list
    |> List.filter (fun x -> x % 2 = 0)
    |> List.length

let evenCountFold list =
    list
    |> List.fold (fun acc x -> if x % 2 = 0 then acc + 1 else acc) 0


// 2
module Tree =
    type 'a Tree =
        | Empty
        | Node of 'a * 'a Tree * 'a Tree

    let rec map f =
        function
        | Empty -> Empty
        | Node(value, left, right) -> Node(f value, map f left, map f right)


// 3
type Expr =
    | Const of int
    | Add of Expr * Expr
    | Mul of Expr * Expr
    | Sub of Expr * Expr
    | Div of Expr * Expr

let rec eval expr =
    match expr with
    | Const c -> c
    | Add (e1, e2) -> eval e1 + eval e2
    | Mul (e1, e2) -> eval e1 * eval e2
    | Sub (e1, e2) -> eval e1 - eval e2
    | Div (e1, e2) -> eval e1 / eval e2


// 4
let rec isPrime n =
    let upperBound = int(sqrt(float n))
    let rec checkDivisor divisor =
        divisor > upperBound || (n % divisor <> 0 && checkDivisor (divisor + 1))
    checkDivisor 2

let primes = Seq.unfold (fun state -> Some(state, state + 1)) 2
             |> Seq.filter isPrime

let actualPrimes = primes |> Seq.take 10 |> Seq.toList
for item in actualPrimes do
    printfn "%d" item
