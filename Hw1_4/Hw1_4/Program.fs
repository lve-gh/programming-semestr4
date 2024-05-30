let factorial n =
    let rec helper n acc =
        match (n, acc) with
        | 0, _ -> Some acc
        | _ when n > 0 -> helper (n - 1) (n * acc)
        | _ -> None
    match n with
    | _ when n >= 0 -> helper n 1
    | _ -> None

let fibonacci n =
    let rec helper n a b =
        match (n, a, b) with
        | 0, res, _ -> Some res
        | _ when n > 0 -> helper (n - 1) b (a + b)
        | _ -> None
    match n with
    | _ when n >= 0 -> helper n 0 1
    | _ -> None

let reversal list =
    let rec reversalT temp =
        function
        | [] -> temp
        | head :: tail -> reversalT (head :: temp) tail

    reversalT [] list

let powList n m =
    let max = int (2.0 ** int (n + m))
    let rec find ls el i =
        if i = 0 then ls
        else find (el :: ls) (el / 2) (i - 1)

    find [] max (m + 1)

let rec findFirst (lst: int list) (num: int) =
    let rec helper lst num index =
        match lst with
        | [] -> None
        | head::tail when head = num -> Some index
        | _::tail -> helper tail num (index + 1)
    helper lst num 0

