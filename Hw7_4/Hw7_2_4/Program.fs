module MiniCrawler

open System
open System.Text.RegularExpressions
open System.Net.Http

let downloadAndPrintLinksInfo (url: string) =
    let httpClient = new HttpClient()
    async {
        let! html = httpClient.GetStringAsync(Uri(url)) |> Async.AwaitTask

        let regex = Regex(@"<a\s+href=""(http[^""]*)""")
        let matches = regex.Matches(html)

        let downloadPage (link: Match) =
            async {
                let linkUrl = link.Groups.[1].Value
                let! pageContent = httpClient.GetStringAsync(Uri(linkUrl)) |> Async.AwaitTask
                printfn "%s — %d" linkUrl pageContent.Length
            }

        let downloadTasks = [ for match_v in matches -> downloadPage match_v ]
        Async.Parallel downloadTasks |> Async.RunSynchronously |> ignore
    }
downloadAndPrintLinksInfo "https://github.com/lve-gh/" |> Async.RunSynchronously

