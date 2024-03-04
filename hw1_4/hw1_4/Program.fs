let factorial n =
    let rec helper n acc =
        match n with
        | 0 -> acc
        | _ when n > 0 -> helper (n - 1) (n * acc)
        | _ -> n
    helper n 1

let fibonacci n =
    let rec helper n a b =
        match n with
        | 0 -> a
        | _ when n > 0 -> helper (n - 1) b (a + b)
        | _ -> n
    helper n 0 1

let reversal list =
    let rec reversalT temp =
        function
        | [] -> temp
        | head :: tail -> reversalT (head :: temp) tail

    reversalT [] list

let pow2 n =
    let rec helper n acc =
        match n with
        | 0 -> acc
        | _ -> helper (n-1) (acc * 2)
    helper n 1

let powList n m =
    match n with
        | _ when n >= 0 && m >= 0 -> [n..m] |> List.map (fun x -> pow2 x)
        | _ -> []

let rec findFirst (lst: int list) (num: int) =
    let rec helper lst num index =
        match lst with
        | [] -> None
        | head::tail when head = num -> Some index
        | _::tail -> helper tail num (index + 1)
    helper lst num 0
