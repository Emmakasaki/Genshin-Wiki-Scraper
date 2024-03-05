using HtmlAgilityPack;
using System.Text;

public class WebScraper
{

    public static async Task Main(string[] args)
    {

        var nameUrl = "https://genshin-impact.fandom.com/wiki/Genshin_Impact_Wiki";
        var nameHttpClient = new HttpClient();
        var nameHtml = await nameHttpClient.GetStringAsync(nameUrl);

        var nameHtmlDocument = new HtmlDocument();
        nameHtmlDocument.LoadHtml(nameHtml);

        var iconUrl = "https://genshin-impact.fandom.com/wiki/Character/List";
        var iconHttpClient = new HttpClient();
        var iconHtml = await iconHttpClient.GetStringAsync(iconUrl);

        var iconHtmlDoc = new HtmlDocument();
        iconHtmlDoc.LoadHtml(iconHtml);

        var names = nameHtmlDocument.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Contains("card-container"));
 
        

        string fileName = "C:\\Users\\username\\source\\repos\\WebScrapperTesting\\WebScrapperTesting\\characters.txt";

        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }

        using (FileStream fs = File.Create(fileName))
        {

            foreach (var div in names)
            {

                var charStr = div.InnerText.Trim();

                var rarStr = "";
                var wepStr = "";
                var elemStr = "";
                
                var charURL = $"https://genshin-impact.fandom.com/wiki/{div.InnerText.Trim()}";

                switch (div.InnerText.Trim())
                {
                    case "Itto":
                        charURL = $"https://genshin-impact.fandom.com/wiki/Arataki_Itto";
                        charStr = "Arataki Itto";
                        break;
                    case "Kokomi":
                        charURL = $"https://genshin-impact.fandom.com/wiki/Sangonomiya_Kokomi";
                        charStr = "Sangonomiya Kokomi";
                        break;
                    case "Raiden":
                        charURL = $"https://genshin-impact.fandom.com/wiki/Raiden_Shogun";
                        charStr = "Raiden Shogun";
                        break;
                    case "Ayaka":
                        charURL = $"https://genshin-impact.fandom.com/wiki/Kamisato_Ayaka";
                        charStr = "Kamisato Ayaka";
                        break;
                    case "Ayato":
                        charURL = $"https://genshin-impact.fandom.com/wiki/Kamisato_Ayato";
                        charStr = "Kamisato Ayato";
                        break;
                    case "Kazuha":
                        charURL = $"https://genshin-impact.fandom.com/wiki/Kaedehara_Kazuha";
                        charStr = "Kaedehara Kazuha";
                        break;
                    case "Traveler":
                        charURL = $"https://genshin-impact.fandom.com/wiki/Traveler";
                        break;
                    case "Heizou":
                        charURL = $"https://genshin-impact.fandom.com/wiki/Shikanoin_Heizou";
                        charStr = "Shikanoin Heizou";
                        break;
                    case "Shinobu":
                        charURL = $"https://genshin-impact.fandom.com/wiki/Kuki_Shinobu";
                        charStr = "Kuki Shinobu";
                        break;
                }

                var charClient = new HttpClient();
                var charHtml = await charClient.GetStringAsync(charURL);
                var charHtmlDoc = new HtmlDocument();
                charHtmlDoc.LoadHtml(charHtml);
                var rarity = charHtmlDoc.DocumentNode.SelectNodes("//img").Where(node => node.GetAttributeValue("alt", "").Contains("Stars"));
                var weapon = charHtmlDoc.DocumentNode.SelectNodes("//td").Where(node => node.GetAttributeValue("data-source", "").Contains("weapon"));
                var element = charHtmlDoc.DocumentNode.SelectNodes("//img").Where(node => node.GetAttributeValue("alt", "").Contains("Element"));

                foreach (var r in rarity)
                {
                    rarStr = r.Attributes["alt"].Value;
                    break;
                }

                foreach (var w in weapon)
                {
                    wepStr = w.InnerText.Trim();
                    break;
                }
                foreach(var e in element)
                {
                    elemStr = e.Attributes["alt"].Value.ToString();
                    elemStr = elemStr.Remove(0,8);
                    break;
                }

                Byte[] character = new UTF8Encoding().GetBytes($"Character: {charStr}\nRarity: {rarStr}\nWeapon: {wepStr}\nElement: {elemStr}\n");
                fs.Write(character, 0, character.Length);
                var imgs = nameHtmlDocument.DocumentNode.SelectNodes("//img").Where(node => node.GetAttributeValue("src", "").Contains($"{div.InnerText.Trim()}_Icon.png"));
                Console.WriteLine($"Character: {charStr}\nRarity: {rarStr}\nWeapon: {wepStr}\nElement: {elemStr}\n");
                int i = 0;
                while (i < 1)
                {

                    iconUrl = $"https://genshin-impact.fandom.com/wiki/{div.InnerText.Trim()}/Gallery?file={div.InnerText.Trim()}_Icon.png";
                    switch (div.InnerText.Trim())
                    {
                        case "Itto":
                            iconUrl = $"https://genshin-impact.fandom.com/wiki/Arataki_Itto/Gallery?file=Arataki_Itto_Icon.png";
                            break;
                        case "Kokomi":
                            iconUrl = $"https://genshin-impact.fandom.com/wiki/Sangonomiya_Kokomi/Gallery?file=Sangonomiya_Kokomi_Icon.png";
                            break;
                        case "Raiden":
                            iconUrl = $"https://genshin-impact.fandom.com/wiki/Raiden_Shogun/Gallery?file=Raiden_Shogun_Icon.png";
                            break;
                        case "Ayaka":
                            iconUrl = $"https://genshin-impact.fandom.com/wiki/Kamisato_Ayaka/Gallery?file=Kamisato_Ayaka_Icon.png";
                            break;
                        case "Ayato":
                            iconUrl = $"https://genshin-impact.fandom.com/wiki/Kamisato_Ayato/Gallery?file=Kamisato_Ayato_Icon.png";
                            break;
                        case "Kazuha":
                            iconUrl = $"https://genshin-impact.fandom.com/wiki/Kaedehara_Kazuha/Gallery?file=Kaedehara_Kazuha_Icon.png";
                            break;
                        case "Traveler":
                            iconUrl = $"https://genshin-impact.fandom.com/wiki/Traveler/Gallery?file=Lumine_Icon.png";
                            break;
                        case "Heizou":
                            iconUrl = $"https://genshin-impact.fandom.com/wiki/Shikanoin_Heizou/Gallery?file=Shikanoin_Heizou_Icon.png";
                            break;
                        case "Shinobu":
                            iconUrl = $"https://genshin-impact.fandom.com/wiki/Kuki_Shinobu/Gallery?file=Kuki_Shinobu_Icon.png";
                            break;
                    }
                    iconHttpClient = new HttpClient();
                    iconHtml = await iconHttpClient.GetStringAsync(iconUrl);
                    iconHtmlDoc.LoadHtml(iconHtml);
                    var icons = iconHtmlDoc.DocumentNode.SelectNodes("//img").Where(node => node.GetAttributeValue("src", "").Contains($"{div.InnerText.Trim()}_Icon.png"));
                    switch (div.InnerText.Trim())
                    {
                        case "Raiden":
                            icons = iconHtmlDoc.DocumentNode.SelectNodes("//img").Where(node => node.GetAttributeValue("src", "").Contains($"Shogun_Icon.png"));
                            break;
                        case "Traveler":
                            icons = iconHtmlDoc.DocumentNode.SelectNodes("//img").Where(node => node.GetAttributeValue("src", "").Contains($"Lumine_Icon.png"));
                            break;
                        case "Yae Miko":
                            icons = iconHtmlDoc.DocumentNode.SelectNodes("//img").Where(node => node.GetAttributeValue("src", "").Contains($"Miko_Icon.png"));
                            break;
                        case "Yun Jin":
                            icons = iconHtmlDoc.DocumentNode.SelectNodes("//img").Where(node => node.GetAttributeValue("src", "").Contains($"Jin_Icon.png"));
                            break;
                        case "Kujou Sara":
                            icons = iconHtmlDoc.DocumentNode.SelectNodes("//img").Where(node => node.GetAttributeValue("src", "").Contains($"Sara_Icon.png"));
                            break;
                        case "Hu Tao":
                            icons = iconHtmlDoc.DocumentNode.SelectNodes("//img").Where(node => node.GetAttributeValue("src", "").Contains($"Tao_Icon.png"));
                            break;


                    }


                    
                    string linkString = "";
                    foreach (var src in icons)
                    {
                        var links = src.Attributes["src"].Value;
                        if (!links.Contains("Discover_Teyvat") && !links.Contains("/wiki/File:"))
                        {
                            Byte[] icon = new UTF8Encoding().GetBytes($"{links}\n\n");
                            fs.Write(icon, 0, icon.Length);
                            Console.WriteLine(links);
                            linkString = links.ToString();
                            break;
                        }

                    }
                    if (linkString != ""){
                        break;
                    }
                    Console.WriteLine($"{linkString}\n");
                    i++;



                }



            }

        }
    }

}