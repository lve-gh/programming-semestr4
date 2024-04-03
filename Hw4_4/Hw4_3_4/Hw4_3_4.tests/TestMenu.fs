module Hw4_3_4.tests

open NUnit.Framework
open System.IO

[<Test>]
let ``Add entry test`` () =
    let phoneBookExcepted = [{ Name = "QWE"; PhoneNumber = "123"}]
    let phoneBook = []
    let fullPhoneBook = addEntry phoneBook "QWE" "123"
    Assert.AreEqual(phoneBookExcepted, fullPhoneBook)

[<Test>]
let ``Find phone by number test`` () =
    let phoneBook = [{ Name = "BOB"; PhoneNumber = "123456"}]
    let result = findPhoneNumberByName phoneBook "BOB"
    Assert.AreEqual(Some "123456", result)
[<Test>]
let ``Find number by phone test`` () =
    let phoneBook = [{ Name = "BOB"; PhoneNumber = "123456"}]
    let result = findNameByPhoneNumber phoneBook "123456"
    Assert.AreEqual(Some "BOB", result)


[<Test>]
let ``Reading and writting test``() =
    let phoneBook = []
    let fileName = "test_file.txt"
    let content = "Hello, World!"
    let fullPhoneBook = addEntry phoneBook "QWE" "123"
    savePhoneBookToFile fullPhoneBook fileName
    let readedPhoneBook = readPhoneBookFromFile "test_file.txt"
    File.Delete("test_file.txt")
    Assert.AreEqual(fullPhoneBook , readedPhoneBook)