module Hw7_2_4.tests

open NUnit.Framework
open MiniCrawler


[<Test>]
       let ``Check the error``() =
            let url = "dsdadasdasd"
            let downloadTasks = downloadAndPrintLinksInfo url |> Async.RunSynchronously
            let errorList : Async<(string * int)> list = [async { return ("error", -1) } ]
            Assert.AreEqual(downloadTasks, errorList);

[<Test>]
        let ``Check the correctness``() =
            let url = "https://github.com/lve-gh/"
            let downloadTasks = downloadAndPrintLinksInfo url |> Async.RunSynchronously
            let expectedList : Async<(string * int)> list = [ async { return ("https://www.githubstatus.com/", 111904) } ]
            Assert.AreEqual(downloadTasks, expectedList);
