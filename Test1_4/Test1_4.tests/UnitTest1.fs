module Test1_4.tests

open NUnit.Framework

[<Test>]
let ``sumOfEvenFibonacci is correct`` () =
    sumOfEvenFibonacci |> should equal 1089154


[<Test>]
let ``Dequeue throw exception`` () =
    let queue = PriorityQueue<int>()
    shouldThrow<InvalidOperationException> (fun () -> queue.Dequeue())

[<Test>]
let ``Enqueue и Dequeue is correct`` () =
    let queue = PriorityQueue<string>()
    queue.Enqueue("1", 5)
    queue.Enqueue("2", 10)
    queue.Enqueue("3", 1)
    
    queue.Dequeue() |> should equal "2" 
    queue.Dequeue() |> should equal "1"
    queue.Dequeue() |> should equal "3" 
