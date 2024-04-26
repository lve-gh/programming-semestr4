open System

let rec fib n =
    match n with
    | 0 | 1 -> n
    | _ -> fib (n - 1) + fib (n - 2)

let isEven x = x % 2 = 0

let sumOfEvenFibonacci = 
    Seq.initInfinite fib 
    |> Seq.filter isEven 
    |> Seq.takeWhile (fun x -> x <= 1000000) 
    |> Seq.sum 

printfn "%d" sumOfEvenFibonacci


let multiplyString str n =
    String.concat "" (Seq.init n (fun _ -> str))


let printSquare n =
    let rec printLine i = 
        if i = 0 then printfn "" 
        else 
            if i = 1 || i = n then printf "*" 
            else printf " " 
            printLine (i - 1)
    
    for line in 1 .. n do
        printLine n 



type PriorityItem<'T> = 
    { Value: 'T
      Priority: int }

type PriorityQueue<'T>() =
    let mutable items = []
    
    member this.Enqueue(value: 'T, priority: int) =
        let newItem = { Value = value; Priority = priority }
        items <- newItem :: items
        items <- List.sortWith (fun i1 i2 -> i2.Priority - i1.Priority) items 
    
    member this.Dequeue() =
        match items with
        | [] -> raise (InvalidOperationException("Очередь пуста"))
        | head :: tail -> 
            items <- tail
            head.Value



