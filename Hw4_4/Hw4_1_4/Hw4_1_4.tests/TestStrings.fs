module Hw4_1_4.tests

open NUnit.Framework

[<Test>]
    let ``CorrectTest``() =
            let check = isValid "{[()]}"
            Assert.AreEqual(check, true)
 
[<Test>]
     let ``IncorrectTest``() =
            let check = isValid "[(]"
            Assert.AreEqual(check, false)