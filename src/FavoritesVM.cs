using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Web.Syndication;

namespace Podcaster
{
    public class FavoritesVM : ObservableObject
    {


        private MediaSource _PlayBarSource;
        public MediaSource PlayBarSource
        {
            get { return _PlayBarSource; }
            set
            {
                if (value != _PlayBarSource)
                {
                    _PlayBarSource = value;
                    OnPropertyChanged("PlayBarSource");
                }
            }
        }
        private ObservableCollection<SearchDisplay> _Favorites = new ObservableCollection<SearchDisplay>();
        public ObservableCollection<SearchDisplay> Favorites
        {
            get { return _Favorites; }
            set
            {
                if (value != _Favorites)
                {
                    _Favorites = value;
                    OnPropertyChanged("Favorites");
                }
            }
        }


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
                                        PlayBarSource = MediaSource.CreateFromUri(episodeUri);
                                        
                                    }
                                }
                            }
                        }

                    }
                }
            }
            catch { }
        }

        public void AddPodToFav(SearchDisplay pod)
        {
            Favorites.Add(pod);
        }
        
    }
}
