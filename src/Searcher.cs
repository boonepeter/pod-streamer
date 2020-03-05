using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Windows.Web.Syndication;

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

        public List<SearchResult> SearchPodcast(string name)
        {
            name = Regex.Replace(name, " ", "+");
            string URL = BaseURL + "search?term=" + name + "&entity=podcast";
            var searches = new List<SearchResult>();
            string text = "";
            using (var webClient = new System.Net.WebClient())
            {
                text = webClient.DownloadString(URL);
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
                            SearchResult searchResult = results[i].ToObject<SearchResult>();
                            searches.Add(searchResult);
                        }
                    }
                }
            }
            return searches;

        }
    }
    public class SearchResult
    {
        public string wrapperType { get; set; }
        public string kind { get; set; }
        public int artistId { get; set; }
        public int collectionId { get; set; }
        public int trackId { get; set; }
        public string artistName { get; set; }
        public string collectionName { get; set; }
        public string trackName { get; set; }
        public string collectionCensoredName { get; set; }
        public string trackCensoredName { get; set; }
        public string artistViewUrl { get; set; }
        public string collectionViewUrl { get; set; }
        public string feedUrl { get; set; }
        public string trackViewUrl { get; set; }
        public string artworkUrl30 { get; set; }
        public string artworkUrl60 { get; set; }
        public string artworkUrl100 { get; set; }
        public double collectionPrice { get; set; }
        public double trackPrice { get; set; }
        public int trackRentalPrice { get; set; }
        public int collectionHdPrice { get; set; }
        public int trackHdPrice { get; set; }
        public int trackHdRentalPrice { get; set; }
        public DateTime releaseDate { get; set; }
        public string collectionExplicitness { get; set; }
        public string trackExplicitness { get; set; }
        public int trackCount { get; set; }
        public string country { get; set; }
        public string currency { get; set; }
        public string primaryGenreName { get; set; }
        public string contentAdvisoryRating { get; set; }
        public string artworkUrl600 { get; set; }
        public List<string> genreIds { get; set; }
        public List<string> genres { get; set; }
    }

}
