using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Podcaster
{
    public class SearchVM : ObservableObject
    {

        private ObservableCollection<BasePodcast> _SearchResults;
        public ObservableCollection<BasePodcast> SearchResults
        {
            get { return _SearchResults; }
            set
            {
                if (value != _SearchResults)
                {
                    _SearchResults = value;
                    OnPropertyChanged("SearchResults");
                }
            }
        }


        private string _SearchText;
        public string SearchText
        {
            get { return _SearchText; }
            set
            {
                if (value != _SearchText)
                {
                    _SearchText = value;
                    OnPropertyChanged("SearchText");
                }
            }
        }

        private ItunesAPI Itunes = new ItunesAPI();
        public FavoritesVM FavVM;

        private MainPage MainP;
        public SearchVM(MainPage mainPage)
        {
            MainP = mainPage;
        }

        public void SearchAddButton_Click(object sender, RoutedEventArgs args)
        {
            try
            {
                FrameworkElement element = (FrameworkElement)args.OriginalSource;
                BasePodcast searchResult = (BasePodcast)element.DataContext;
                FavVM.AddPodToFav(searchResult);
                searchResult.InLibrary = true;
            }
            catch
            {
                string peter = "error";
            }
        }

        public void SearchTextBox_QuerySubmitted()
        {
            List<BasePodcast> results = Itunes.SearchPodcast(SearchText);

            SearchResults = new ObservableCollection<BasePodcast>(results);
            for (int i = 0; i < SearchResults.Count; i++)
            {
                if (FavVM.Favorites.Podcasts.Any(x => x.collectionId == SearchResults[i].collectionId))
                {
                    SearchResults[i].InLibrary = true;
                }
            }
        }
    }
}
