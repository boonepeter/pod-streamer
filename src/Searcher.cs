using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using System.Xml;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Web.Syndication;
using Windows.UI.Xaml.Controls;

namespace Podcaster
{
    public class Searcher
    {

        public static List<Episode> GetAllEpisodes(string feedURL)
        {
            List<Episode> episodes = new List<Episode>();
            using (var webClient = new WebClient())
            {
                var rss = webClient.DownloadString(feedURL);
                SyndicationFeed feed = new SyndicationFeed();
                feed.Load(rss);

                foreach (var item in feed.Items)
                {
                    Episode ep = new Episode();
                    ep.Title = item.Title.Text;
                    ep.pubDate = item.PublishedDate;
                    ep.summary = item.Summary.Text;
                    


                    foreach (var value in item.GetXmlDocument(SyndicationFormat.Rss20).ChildNodes)
                    {
                        var peter = value;
                        foreach (var child in peter.ChildNodes)
                        {
                            var hello = child;
                            switch (hello.NodeName)
                            {
                                case "title":
                                    break;
                                case "description":
                                    break;
                                case "enclosure":
                                    break;
                                case "guid":
                                    break;
                                case "pubDate":
                                    break;
                                case "published":
                                    break;
                                case "updated":
                                    break;
                                case "episodeType":
                                    break;
                                case "author":
                                    break;
                                case "subtitle":
                                    break;
                                case "summary":
                                    break;
                                case "duration":
                                    bool success = false;
                                    TimeSpan parsed = new TimeSpan();
                                    if (child.InnerText.Contains(":"))
                                    {
                                        string[] times = child.InnerText.Split(':');
                                        if (times.Length == 2)
                                        {
                                            if (Int32.TryParse(times[0], out int mins) && Int32.TryParse(times[1], out int secs))
                                            {
                                                success = true;
                                                parsed = new TimeSpan(0, mins, secs);
                                            }
                                        }
                                        else
                                        {
                                            success = TimeSpan.TryParse(child.InnerText, out parsed);
                                        }
                                    }
                                    else
                                    {
                                        success = Double.TryParse(child.InnerText, out double dParsed);
                                        parsed = TimeSpan.FromSeconds(dParsed);
                                    }
                                    if (success)
                                    {
                                        ep.duration = parsed;
                                    }
                                    else
                                    {
                                        string helloworld = "catch";
                                    }
                                    break;
                                case "explicit":
                                    break;
                                case "image":
                                    break;
                                case "season":
                                    break;
                                case "episode":
                                    break;
                                case "link":
                                    break;
                                case "keywords":
                                    break;
                                case "category":
                                    break;
                                case "content":
                                    break;
                                case "thumbnail":
                                    break;
                                default:
                                    break;
                            }
                        }

                    }
                    if (item.Links.Count > 0)
                    {
                        var links = item.Links;
                        foreach (var link in links)
                        {
                            if (link.NodeName == "enclosure")
                            {
                                ep.StreamURL = link.Uri.AbsoluteUri;
                                episodes.Add(ep);
                                break;
                            }
                        }
                    }
                }

            }
            return episodes;

        }
    }



    public class ItunesAPI
    {
        private const string BaseURL = "https://itunes.apple.com/";
        public ItunesAPI()
        {

        }

        public List<BasePodcast> SearchPodcast(string name)
        {
            name = Regex.Replace(name, " ", "+");
            string URL = BaseURL + "search?term=" + name + "&entity=podcast";
            var searches = new List<BasePodcast>();
            string text = "";
            try
            {
                using (var webClient = new System.Net.WebClient())
                {
                    text = webClient.DownloadString(URL);
                }
            }
            catch (WebException e)
            {
                return searches;
            }

            Newtonsoft.Json.Linq.JObject output = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(text);
            foreach (var child in output.Children())
            {
                if (child.HasValues)
                {
                    if (child.Path == "resultCount")
                    {
                        continue;
                    }
                    else if (child.Path == "results")
                    {
                        var results = child.First;
                        for (int i = 0; i < results.Count(); i++)
                        {
                            BasePodcast searchResult = results[i].ToObject<BasePodcast>();
                            searches.Add(searchResult);
                        }
                    }
                }
            }
            return searches;

        }
    }


}
