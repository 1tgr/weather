#I @"packages\HtmlAgilityPack\lib"
#I @"packages\MSDN.FSharpChart.dll\lib"
#r "HtmlAgilityPack.dll"
#r "MSDN.FSharpChart.dll"
#r "System.Windows.Forms.DataVisualization"
open System
open System.IO
open System.Net
open System.Windows.Forms
open HtmlAgilityPack
open Microsoft.FSharp.Reflection
open MSDN.FSharp.Charting
open MSDN.FSharp.Charting.ChartTypes

fsi.AddPrinter <| fun ch ->
    let frm = new Form(Visible = true, TopMost = true, Width = 700, Height = 500)
    let ctl = new ChartControl(ch, Dock = DockStyle.Fill)
    frm.Controls.Add(ctl)
    frm.Show()
    "(Chart)"

let urls =
    [|
        "001/m001.htm", "China"
        "002/c00001.htm", "Hong Kong, China"
        "003/m003.htm", "Portugal"
        "004/m004.htm", "Madagascar"
        "005/m005.htm", "Bosnia and Herzegovina"
        "006/m006.htm", "Austria"
        "007/m007.htm", "Azerbaijan"
        "008/m008.htm", "Norway"
        "009/m009.htm", "Poland"
        "010/m010.htm", "United Kingdom of Great Britain and Northern Ireland"
        "011/m011.htm", "Slovakia"
        "012/c00042.htm", "Malta"
        "013/m013.htm", "Israel"
        "014/m014.htm", "Turkey"
        "015/c00054.htm", "Slovenia"
        "016/m016.htm", "Germany"
        "017/m017.htm", "Hungary"
        "018/m018.htm", "Armenia"
        "019/m019.htm", "Croatia"
        "020/m020.htm", "Malaysia"
        "021/m021.htm", "Philippines"
        "022/m022.htm", "Fiji"
        "023/c00095.htm", "Brunei Darussalam"
        "024/c00096.htm", "British Caribbean Territories"
        "025/m025.htm", "Bahamas"
        "026/m026.htm", "Jamaica"
        "027/m027.htm", "Trinidad and Tobago"
        "028/m028.htm", "Chile"
        "029/m029.htm", "Peru"
        "030/m030.htm", "Oman"
        "031/c00114.htm", "Nepal"
        "032/m032.htm", "Uzbekistan"
        "033/m033.htm", "Mozambique"
        "034/m034.htm", "Mali"
        "035/m035.htm", "South Africa"
        "036/c00140.htm", "Gambia"
        "037/m037.htm", "Netherlands"
        "038/m038.htm", "Belize"
        "039/c00149.htm", "Estonia"
        "040/m040.htm", "Zambia"
        "041/c00151.htm", "Antigua and Barbuda"
        "042/m042.htm", "Eritrea"
        "043/m043.htm", "Indonesia"
        "044/m044.htm", "Latvia"
        "045/m045.htm", "Morocco"
        "046/m046.htm", "New Zealand"
        "047/m047.htm", "Pakistan"
        "048/c00244.htm", "Senegal"
        "049/m049.htm", "United Republic of Tanzania"
        "050/m050.htm", "Argentina"
        "051/c00220.htm", "Bahrain"
        "053/m053.htm", "Belgium"
        "055/m055.htm", "Cameroon"
        "056/m056.htm", "Canada"
        "057/m057.htm", "Colombia"
        "058/c00311.htm", "Comoros"
        "059/m059.htm", "Egypt"
        "060/m060.htm", "Ethiopia"
        "061/m061.htm", "Finland"
        "062/m062.htm", "France"
        "063/m063.htm", "Greece"
        "064/c00346.htm", "Guinea-Bissau"
        "065/m065.htm", "Guyana"
        "066/m066.htm", "India"
        "067/m067.htm", "Ireland"
        "068/m068.htm", "Japan"
        "069/m069.htm", "Jordan"
        "070/m070.htm", "Kazakhstan"
        "071/m071.htm", "Kenya"
        "072/c00156.htm", "Macao, China"
        "073/c00262.htm", "Malawi"
        "074/c00327.htm", "Niger"
        "075/m075.htm", "Nigeria"
        "076/m076.htm", "Panama"
        "077/m077.htm", "Papua New Guinea"
        "079/m079.htm", "Saudi Arabia"
        "080/c00253.htm", "Seychelles"
        "081/c00234.htm", "Singapore"
        "082/m082.htm", "Viet Nam, Socialist Republic of"
        "083/m083.htm", "Spain"
        "084/m084.htm", "Sri Lanka"
        "085/m085.htm", "Sudan"
        "087/m087.htm", "Switzerland"
        "088/m088.htm", "Chad"
        "089/m089.htm", "Thailand"
        "090/m090.htm", "The former Yugoslav Republic of Macedonia"
        "091/c00260.htm", "Togo"
        "092/c00207.htm", "Ukraine"
        "093/m093.htm", "United States of America"
        "094/m094.htm", "Uruguay"
        "095/m095.htm", "Republic of Korea"
        "096/m096.htm", "Sweden"
        "097/c00189.htm", "Iceland"
        "098/c00192.htm", "Luxembourg"
        "099/m099.htm", "Syrian Arab Republic"
        "100/c00196.htm", "Cape Verde"
        "101/m101.htm", "Republic of Serbia"
        "103/m103.htm", "Bulgaria"
        "104/m104.htm", "Cyprus"
        "105/m105.htm", "Lithuania"
        "106/c00205.htm", "Belarus"
        "107/m107.htm", "Russian Federation"
        "108/m108.htm", "Republic of Moldova"
        "109/c00209.htm", "Georgia"
        "110/m110.htm", "Tajikistan"
        "111/c00212.htm", "Turkmenistan"
        "112/c00214.htm", "Lebanon"
        "113/m113.htm", "Kuwait"
        "114/m114.htm", "Iran, Islamic Republic of"
        "115/m115.htm", "Afghanistan"
        "116/c00221.htm", "Qatar"
        "117/m117.htm", "United Arab Emirates"
        "118/m118.htm", "Maldives"
        "119/m119.htm", "Mongolia"
        "120/c00230.htm", "Democratic People's Republic of Korea"
        "121/m121.htm", "Lao People's Democratic Republic"
        "122/m122.htm", "Algeria"
        "123/c00243.htm", "Tunisia"
        "124/m124.htm", "Guinea"
        "125/c00246.htm", "Sierra Leone"
        "126/c00250.htm", "Djibouti"
        "127/m127.htm", "Rwanda"
        "128/c00255.htm", "Gabon"
        "129/c00261.htm", "Angola"
        "130/m130.htm", "Zimbabwe"
        "131/m131.htm", "Cuba"
        "132/m132.htm", "Dominican Republic"
        "133/c00283.htm", "Honduras"
        "134/c00287.htm", "Dominica"
        "135/m135.htm", "Venezuela"
        "136/m136.htm", "Brazil"
        "137/c00291.htm", "Ecuador"
        "138/m138.htm", "Paraguay"
        "139/c00295.htm", "Solomon Islands"
        "140/m140.htm", "New Caledonia"
        "141/m141.htm", "Bangladesh"
        "142/m142.htm", "Botswana"
        "143/m143.htm", "Burkina Faso"
        "144/m144.htm", "Burundi"
        "145/m145.htm", "Cambodia"
        "151/m151.htm", "Ghana"
        "152/c00906.htm", "Guatemala"
        "153/c01500.htm", "Haiti"
        "154/m154.htm", "Iraq"
        "155/m155.htm", "Lesotho"
        "157/m157.htm", "Libya"
        "164/m164.htm", "Saint Lucia"
        "166/m166.htm", "Swaziland"
        "168/m168.htm", "Uganda"
        "169/m169.htm", "Vanuatu"
        "170/c00259.htm", "Benin"
        "171/m171.htm", "Costa Rica"
        "172/m172.htm", "Czech Republic"
        "173/m173.htm", "Denmark"
        "174/m174.htm", "El Salvador"
        "175/c00297.htm", "French Polynesia"
        "176/m176.htm", "Italy"
        "177/c00210.htm", "Kyrgyzstan"
        "178/m178.htm", "Mauritius"
        "179/m179.htm", "Mexico"
        "180/m180.htm", "Myanmar"
        "181/c00286.htm", "Netherlands Antilles and Aruba"
        "182/c00284.htm", "Nicaragua"
        "183/c00200.htm", "Romania"
        "184/c01230.htm", "Samoa"
        "185/m185.htm", "Australia"
        "186/m186.htm", "Spain - Canary Islands"
        "187/c00305.htm", "France - Guiana"
        "188/c00306.htm", "France - Guadeloupe"
        "189/c00307.htm", "France - Martinique"
        "190/c00005.htm", "Portugal - Madeira"
        "191/m191.htm", "Bhutan"
        "193/c01378.htm", "Montenegro"
        "194/c01372.htm", "UK - Jersey"
        "195/c01373.htm", "UK - Guernsey"
        "197/c01377.htm", "UK - Bermuda"
        "198/c01374.htm", "UK - Isle of Man"
    |]

let select xpath (node : HtmlNode) =
    match node.SelectNodes(xpath) with
    | null -> [ ]
    | nodes -> List.ofSeq nodes

type Month =
    | Jan
    | Feb
    | Mar
    | Apr
    | May
    | Jun
    | Jul
    | Aug
    | Sep
    | Oct
    | Nov
    | Dec

type ClimateField =
    | MinTemperature
    | MaxTemperature
    | Rainfall
    | RainDays

let parseClimate : HtmlDocument -> Map<Month * ClimateField, double> =
    let parse field s =
        match s, Double.TryParse(s) with
        | ("Trace" | "TR" | "Nil" | "/"), (_, _) -> 0.0
        | _, (true, d) -> d
        | _, (false, _) -> failwithf "Can't parse %s = %s" field s

    let parseMonth =
        let cases = [
            for case in FSharpType.GetUnionCases(typeof<Month>) ->
                case.Name, unbox (FSharpValue.MakeUnion(case, [| |]))
        ]

        let picker s (name, case) =
            if s = name
            then Some case
            else None

        fun s -> List.pick (picker s) cases

    fun doc ->
        Map.ofSeq <| seq {
            for row in select "//table[tr/td[@class='climat_header']]/tr" doc.DocumentNode do
                match select "td" row with
                | [ month; minTemperature; maxTemperature; rainfall; rainDays ] ->
                    let month = parseMonth (month.InnerText.Trim())
                    yield (month, MinTemperature), (parse "MinTemperature" minTemperature.InnerText)
                    yield (month, MaxTemperature), (parse "MaxTemperature" maxTemperature.InnerText)
                    yield (month, Rainfall), (parse "Rainfall" rainfall.InnerText)
                    yield (month, RainDays), (parse "RainDays" rainDays.InnerText)

                | _ -> ()
        }

let keys m = Set.ofSeq (Seq.map fst (Map.toSeq m))

let pearson p1 p2 =
    match Set.intersect (keys p1) (keys p2) with
    | si when not (Set.isEmpty si) ->
        let sum1 = List.sum [for it in si -> p1.[it]]
        let sum2 = List.sum [for it in si -> p2.[it]]

        let sum1Sq = List.sum [for it in si -> pown p1.[it] 2]
        let sum2Sq = List.sum [for it in si -> pown p2.[it] 2]

        let pSum = List.sum [for it in si -> p1.[it] * p2.[it]]

        let n = double (Set.count si)
        let num = pSum - (sum1 * sum2 / n)
        match sqrt ((sum1Sq - (pown sum1 2) / n) * (sum2Sq - (pown sum2 2) / n)) with
        | 0.0 -> 0.0
        | den -> num / den

    | _ -> 0.0

let sim climates city1 city2 =
    let picker s (key : string) =
        match key.IndexOf(s, StringComparison.InvariantCultureIgnoreCase) with
        | -1 -> fun _ -> None
        | _ -> Some

    let p1 = Map.pick (picker city1) climates
    let p2 = Map.pick (picker city2) climates
    pearson p1 p2

let climates =
    let client = HtmlWeb(CachePath = Path.GetFullPath(@"C:\Users\Tim\Git\weather\cache"), UsingCache = true, CacheOnly = true)
    let baseUrl = Uri("http://www.worldweather.org")

    let mapper i (countryUrl : string, countryDesc) =
        seq {
            let countryUrl = Uri(baseUrl, countryUrl)
            printf "[%d/%d] %s ... " (i + 1) (Array.length urls) countryDesc
            stdout.Flush()
            let countryDoc = client.Load(string countryUrl)

            match parseClimate countryDoc with
            | c when not (Map.isEmpty c) -> yield (countryDesc, None), c
            | _ -> ()

            for node in select "//a[@class='text15']" countryDoc.DocumentNode do
                let cityUrl = Uri(countryUrl, node.GetAttributeValue("href", ""))
                let cityDesc = node.InnerText
                printf "%s " cityDesc
                stdout.Flush()
                let cityDoc = client.Load(string cityUrl)
                match parseClimate cityDoc with
                | c when not (Map.isEmpty c) -> yield (countryDesc, Some cityDesc), c
                | _ -> ()

            printfn "done"
        }

    urls
    |> Seq.sortBy snd
    |> Seq.mapi mapper
    |> Seq.concat
    |> Map.ofSeq

let correlations climate1 =
    climates
    |> Map.map (fun _ -> pearson climate1)
    |> Map.toSeq
    |> Seq.sortBy (fun (_, correl) -> -correl)
    |> Seq.truncate 10
    |> List.ofSeq

let london = "United Kingdom of Great Britain and Northern Ireland", Some "LONDON"
let likeLondon = correlations climates.[london]

let nicePlace factors =
    let factors = Map.ofSeq factors
    let mapper (_, field) = defaultArg (Map.tryFind field factors) id
    Map.map mapper climates.[london]

let compare label1 label2 =
    let label =
        function
        | country, Some city -> sprintf "%s, %s" city country
        | country, None -> country

    let climate1 = climates.[label1]
    let climate2 = climates.[label2]

    let label1 = label label1
    let label2 = label label2

    let data field map : (_ * double) list =
        let chooser =
            function
            | (x, f), y when f = field -> Some (x, y)
            | _ -> None

        List.choose chooser (Map.toList map)

    let formatX data =
        [ for x, y in data -> sprintf "%A" x, y ]

    let range minField maxField =
        let min1 = Map.ofList (data minField climate1)
        let max1 = Map.ofList (data maxField climate1)
        let min2 = Map.ofList (data minField climate2)
        let max2 = Map.ofList (data maxField climate2)
        let data1 = Seq.map (fun k -> k, (min1.[k], max1.[k])) (Set.intersect (keys min1) (keys max1))
        let data2 = Seq.map (fun k -> k, (min2.[k], max2.[k])) (Set.intersect (keys min2) (keys max2))
        [
            data1 |> formatX |> FSharpChart.RangeColumn |> FSharpChart.WithLegend(label1) :> GenericChart
            data2 |> formatX |> FSharpChart.RangeColumn |> FSharpChart.WithLegend(label2) :> GenericChart
        ]
        |> FSharpChart.Combine
        |> FSharpChart.WithArea.AxisY(Title = sprintf "%A/%A" minField maxField)

    let column field =
        [
            data field climate1 |> formatX |> FSharpChart.Column |> FSharpChart.WithLegend(label1) :> GenericChart
            data field climate2 |> formatX |> FSharpChart.Column |> FSharpChart.WithLegend(label2) :> GenericChart
        ]
        |> FSharpChart.Combine
        |> FSharpChart.WithArea.AxisY(Title = sprintf "%A" field)

    [
        range MinTemperature MaxTemperature :> GenericChart
        column Rainfall :> GenericChart
        column RainDays :> GenericChart
    ]
    |> FSharpChart.Rows

correlations (nicePlace [ RainDays, (*) 0.2; Rainfall, (*) 0.5 ]) |> List.head ||> fun name _ -> compare london name
