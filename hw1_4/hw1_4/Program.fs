let rec factorial x = 
    if x = 1 then 1 else x * factorial(x - 1)


let rec fibonacci x =
    match x with
    | 1 | 2 -> 1
    | x -> fibonacci(x - 1) + fibonacci(x - 2)

let reversal list =
    let rec reversalT temp =
        function
        | [] -> temp
        | head :: tail -> reversalT (head :: temp) tail

    reversalT [] list

let listPow n m =
    List.unfold(fun (x, c) -> 
    if c < 0 
        then None 
    else 
        Some(x, (x * 2, c - 1))) (pown 2 n, m - n)

let rec findFirst (lst: int list) (num: int) =
    let rec findHelper lst num index =
        match lst with
        | [] -> None
        | head::tail when head = num -> Some index
        | _::tail -> findHelper tail num (index + 1)
    findHelper lst num 0
