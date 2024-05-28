module Hw4_2_4

let func x l = List.map (fun y -> y * x) l
//let func x l = List.map (fun y -> y * x) l
//let func x = List.map (fun y -> y * x)
//let func x = List.map (fun y -> y * x)
//let func x = List.map (fun y -> (*) y x)
//let func x = List.map << (fun y -> (*) y) <| x
//let func = List.map << (fun y -> (*) y)
let funcPF = List.map << (*)