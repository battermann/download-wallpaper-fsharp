#r @"packages/FSharp.Data/lib/net40/FSharp.Data.dll"

open System
open System.IO

module BingWallpaper =
    open FSharp.Data
    
    type Wallpaper = JsonProvider<"data.json">
    
    let downloadUrl = 
        let url = @"http://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=en-US"
        let data = Wallpaper.Load(url);    
        sprintf @"http://bing.com%s" (data.Images |> Array.head).Url

module Web =
    open System.Net

    let downloadFile (url:String) (dest:String) =
        use wc = new WebClient()
        wc.DownloadFile(url, dest)

module Helper =
    let (</>) a b = Path.Combine(a,b)

module App =
    open Helper

    let run =
        let fileName = __SOURCE_DIRECTORY__ </> sprintf @"images/%s.jpg" (Guid.NewGuid().ToString())
        do Directory.CreateDirectory(__SOURCE_DIRECTORY__ </> @"images/") |> ignore
        do Web.downloadFile BingWallpaper.downloadUrl fileName
        do printfn @"%s" fileName

App.run