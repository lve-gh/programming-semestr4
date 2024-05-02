module Hw7_2_4.tests

open NUnit.Framework
open MiniCrawler
[<Test>]
    Assert.AreEqual(downloadAndPrintLinksInfo "https://github.com/lve-gh/"  |> Async.RunSynchronously, [("https://www.githubstatus.com/", 111477)] );