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


let drawFigure n =
    let printRow row =
        printfn "%s" row

    let drawTopBottomRow n =
        printRow (String.replicate n "*")

    let drawMiddleRow n =
        printRow ("*" + String.replicate (n-2) " " + "*")

    drawTopBottomRow n
    for _ in 1..(n-2) do
        drawMiddleRow n
    drawTopBottomRow n

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



