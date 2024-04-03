module Hw4_3_4

open System

type PhoneBookEntry = { Name: string; PhoneNumber: string }
type PhoneBook = PhoneBookEntry list

let addEntry (phoneBook: PhoneBook) (name: string) (phoneNumber: string) =
    phoneBook @ [{ Name = name; PhoneNumber = phoneNumber }]

let findPhoneNumberByName (phoneBook: PhoneBook) (name: string) =
    phoneBook |> List.tryFind (fun entry -> entry.Name = name) |> Option.map (fun entry -> entry.PhoneNumber)

let findNameByPhoneNumber (phoneBook: PhoneBook) (phoneNumber: string) =
    phoneBook |> List.tryFind (fun entry -> entry.PhoneNumber = phoneNumber) |> Option.map (fun entry -> entry.Name)

let printPhoneBook (phoneBook: PhoneBook) =
    phoneBook |> List.iter (fun entry -> printfn "%s - %s" entry.Name entry.PhoneNumber)

let savePhoneBookToFile (phoneBook: PhoneBook) (filePath: string) =
    System.IO.File.WriteAllLines(filePath, phoneBook |> List.map (fun entry -> sprintf "%s,%s" entry.Name entry.PhoneNumber))

let readPhoneBookFromFile (filePath: string) =
    match System.IO.File.Exists(filePath) with
    | true -> System.IO.File.ReadAllLines(filePath) |> Array.map (fun line -> 
                    match line.Split(',') with
                    | [|name; phoneNumber|] -> { Name = name; PhoneNumber = phoneNumber }
                    | _ -> failwith "Invalid file format") |> Array.toList
    | false -> []

let rec interactivePhoneBook (phoneBook: PhoneBook) =
    printfn "Choose an option:"
    printfn "1. Add entry"
    printfn "2. Find phone number by name"
    printfn "3. Find name by phone number"
    printfn "4. Print phone book"
    printfn "5. Save phone book to file"
    printfn "6. Read phone book from file"
    printfn "7. Exit"
    
    let choice = Console.ReadLine()

    match choice with
    | "1" ->
        printfn "Enter name:"
        let name = Console.ReadLine()
        printfn "Enter phone number:"
        let phoneNumber = Console.ReadLine()
        interactivePhoneBook (addEntry phoneBook name phoneNumber)
    | "2" ->
        printfn "Enter name to find its phone number:"
        let name = Console.ReadLine()
        match findPhoneNumberByName phoneBook name with
        | Some phoneNumber -> printfn "Phone number: %s" phoneNumber
        | None -> printfn "Name not found"
        interactivePhoneBook phoneBook
    | "3" ->
        printfn "Enter phone number to find its name:"
        let phoneNumber = Console.ReadLine()
        match findNameByPhoneNumber phoneBook phoneNumber with
        | Some name -> printfn "Name: %s" name
        | None -> printfn "Phone number not found"
        interactivePhoneBook phoneBook
    | "4" ->
        printfn "Phone book entries:"
        printPhoneBook phoneBook
        interactivePhoneBook phoneBook
    | "5" ->
        printfn "Enter file path to save phone book:"
        let filePath = Console.ReadLine()
        savePhoneBookToFile phoneBook filePath
        printfn "Phone book saved to file."
        interactivePhoneBook phoneBook
    | "6" ->
        printfn "Enter file path to read phone book from:"
        let filePath = Console.ReadLine()
        let newPhoneBook = readPhoneBookFromFile filePath
        printfn "Phone book read from file."
        interactivePhoneBook newPhoneBook
    | "7" -> printfn "Exiting..."
    | _ -> printfn "Invalid option. Please try again." 
           interactivePhoneBook phoneBook

let initialPhoneBook = []
interactivePhoneBook initialPhoneBook