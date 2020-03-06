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
        private ObservableCollection<Episode> _CurrentPodcastEpisodes;
        public ObservableCollection<Episode> CurrentPodcastEpisodes
        {
            get { return _CurrentPodcastEpisodes; }
            set
            {
                if (value != _CurrentPodcastEpisodes)
                {
                    _CurrentPodcastEpisodes = value;
                    OnPropertyChanged("CurrentPodcastEpisodes");
                }
            }
        }

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

        private MainPage MainP;
        public FavoritesVM(MainPage mainPage)
        {
            MainP = mainPage;
        }

        public void FavoritesListBox_Play(object sender, RoutedEventArgs args)
        {
            try
            {
                FrameworkElement element = (FrameworkElement)sender;
                if (element.DataContext.GetType() == typeof(Episode))
                {
                    Episode ep = (Episode)element.DataContext;

                    MainP.PlayFromSource(new Uri(ep.StreamURL));
                }
            }
            catch { }
        }

        public void AddPodToFav(SearchDisplay pod)
        {
            Favorites.Add(pod);
        }

        public void FavoritesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender.GetType() == typeof(ListView))
            {
                ListView list = (ListView)sender;
                if (list.SelectedItem != null && list.SelectedItem.GetType() == typeof(SearchDisplay))
                {
                    SearchDisplay pod = (SearchDisplay)list.SelectedItem;

                    var episodes = Searcher.GetAllEpisodes(pod.FeedURL);
                    CurrentPodcastEpisodes = new ObservableCollection<Episode>(episodes);
                }
            }
        }
    }
}
