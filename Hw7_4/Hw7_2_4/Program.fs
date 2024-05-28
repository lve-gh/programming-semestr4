module MiniCrawler

open System
open System.Text.RegularExpressions
open System.Net.Http


let printAsyncList (asyncList: Async<string> list) =
    async {
        for asyncString in asyncList do
            let! str = asyncString
            printfn "%s" str
    }

let downloadAndPrintLinksInfo (url: string) =
    let httpClient = new HttpClient()
    async {
        let! html = httpClient.GetStringAsync(url) |> Async.AwaitTask

        let regex = new Regex(@"<a\s+href=""(http[^""]*)""", RegexOptions.Compiled)
        let matches = regex.Matches(html)

        let downloadPage (link: Match) =
            async {
                let linkUrl = link.Groups.[1].Value
                let! pageContent = httpClient.GetStringAsync(Uri(linkUrl)) |> Async.AwaitTask
                return linkUrl + " " + pageContent.Length.ToString()
            }
        let downloadTasks = [ for match_v in matches -> downloadPage match_v ]
        return downloadTasks
    }
let a = downloadAndPrintLinksInfo "https://github.com/lve-gh/" |> Async.RunSynchronously
//printfn "%s" a[0]
printAsyncList a |> Async.RunSynchronously

