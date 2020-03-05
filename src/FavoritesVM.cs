using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcaster
{
    class FavoritesVM
    {
        /*

        public void FavoritesListBox_Play(object sender, RoutedEventArgs args)
        {
            try
            {
                Button button = (Button)sender;
                if (button.DataContext.GetType() == typeof(SearchDisplay))
                {
                    SearchDisplay displayedItem = (SearchDisplay)button.DataContext;
                    using (var webClient = new WebClient())
                    {
                        var rss = webClient.DownloadString(displayedItem.FeedURL);
                        SyndicationFeed feed = new SyndicationFeed();
                        feed.Load(rss);
                        bool toBreak = false;

                        foreach (var item in feed.Items)
                        {
                            if (toBreak) break;
                            if (item.Links.Count > 0)
                            {
                                var links = item.Links;
                                foreach (var link in links)
                                {
                                    if (link.NodeName == "enclosure")
                                    {
                                        Uri episodeUri = link.Uri;
                                        toBreak = true;
                                        using (var client = new WebClient())
                                        {
                                            SearchMedia.Source = MediaSource.CreateFromUri(episodeUri);
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }
            catch { }
        }
        */

    }
}
