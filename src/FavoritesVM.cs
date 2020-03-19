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


        private BasePodcast _SelectedPodcast;
        public BasePodcast SelectedPodcast
        {
            get { return _SelectedPodcast; }
            set
            {
                if (value != _SelectedPodcast)
                {
                    _SelectedPodcast = value;
                    OnPropertyChanged("SelectedPodcast");
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
            OpenFavorites();
        }

        public void FavoritesListBox_Play(object sender, RoutedEventArgs args)
        {
            try
            {
                FrameworkElement element = (FrameworkElement)args.OriginalSource;
                Episode ep = (Episode)element.DataContext;
                MainP.PlayFromSource(new Uri(ep.StreamURL));
                ep.HasListened = true;
            }
            catch { }
        }

        public void AddPodToFav(BasePodcast pod)
        {
            Favorites.Podcasts.Add(pod);
        }

        public async void FavoritesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var pod = SelectedPodcast;
            if (pod != null)
            {
                var eps = await Task.Run(() => pod?.UpdateEpisodes());
                if (pod.Episodes is null || pod.Episodes.Count != eps.Count)
                {
                    pod.Episodes = new ObservableCollection<Episode>(eps);
                }
            }
        }

        public void RemovePodcast(object sender, RoutedEventArgs args)
        {
            try
            {
                FrameworkElement el = (FrameworkElement)args.OriginalSource;
                Favorites.Podcasts.Remove((BasePodcast)el.DataContext);
            }
            catch
            {
                string peter = "error";
            }
        }

        public async void OpenFavorites()
        {
            var lib = await Library.Open("userData.xml");
            if (lib != null)
            {
                Favorites = lib;
            }
            else
            {
                Favorites = new Library();
            }
        }
    }
}
