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

        private Library _Favorites;
        public Library Favorites
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
            Favorites = new Library();
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

        public void AddPodToFav(BasePodcast pod)
        {
            Favorites.Podcasts.Add(pod);
        }

        public void FavoritesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender.GetType() == typeof(ListView))
            {
                ListView list = (ListView)sender;
                if (list.SelectedItem != null && list.SelectedItem.GetType() == typeof(BasePodcast))
                {
                    BasePodcast pod = (BasePodcast)list.SelectedItem;
                    if (pod.Episodes != null && pod.Episodes.Count > 0)
                    {
                        CurrentPodcastEpisodes = pod.Episodes;
                    }
                    else
                    {
                        pod.Episodes = new ObservableCollection<Episode>(Searcher.GetAllEpisodes(pod.feedUrl));
                        CurrentPodcastEpisodes = pod.Episodes;
                    }
                }
            }
        }
    }
}
