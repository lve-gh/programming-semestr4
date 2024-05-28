module Hw7_2_4.tests

open NUnit.Framework
open MiniCrawler


[<Test>]
      let ``Should return a list of download tasks``() =
        let url = "https://github.com/lve-gh/"
        let downloadTasks = downloadAndPrintLinksInfo url |> Async.RunSynchronously
        Assert.IsInstanceOf<Async<string> list>(downloadTasks)

[<Test>]
       let ``Return value is not empty``() =
         let url = "https://github.com/lve-gh/"
         let downloadTasks = downloadAndPrintLinksInfo url |> Async.RunSynchronously
         Assert.IsNotEmpty(downloadTasks)