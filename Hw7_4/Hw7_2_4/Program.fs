module MiniCrawler

open System
open System.Text.RegularExpressions
open System.Net.Http

let printLinksInfo (links: Async<(string * int)> list) =
    links |> List.iter (fun link -> 
        Async.RunSynchronously link
        |> fun (url, length) -> printfn "%s %d" url length
    )


let downloadAndPrintLinksInfo (url: string) =
    let httpClient = new HttpClient()
    async {
        try
        let! html = httpClient.GetStringAsync(url) |> Async.AwaitTask

        let regex = new Regex(@"<a\s+href=""(http[^""]*)""", RegexOptions.Compiled)
        let matches = regex.Matches(html)

        let downloadPage (link: Match) =
            async {
                try
                    let linkUrl = link.Groups.[1].Value
                    let! pageContent = httpClient.GetStringAsync(Uri(linkUrl)) |> Async.AwaitTask
                    return (linkUrl, pageContent.Length)
                with _ ->
                    return ("error", -1)
            }
        let downloadTasks = [ for match_v in matches -> downloadPage match_v ]
        return downloadTasks
        with _ ->
            let errorList : Async<(string * int)> list = [ async { return ("error", -1) } ]
            return errorList
    }
let a = downloadAndPrintLinksInfo "https://github.com/lve-gh/" |> Async.RunSynchronously
printLinksInfo a

