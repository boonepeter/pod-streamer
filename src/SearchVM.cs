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

        public void SearchAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(Button))
            {
                Button button = (Button)sender;
                if (button.DataContext.GetType() == typeof(BasePodcast))
                {
                    BasePodcast searchResult = (BasePodcast)button.DataContext;
                    FavVM.AddPodToFav(searchResult);
                }
            }

        }


        public void SearchTextBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            List<BasePodcast> results = Itunes.SearchPodcast(sender.Text);
            ObservableCollection<BasePodcast> listboxItems = new ObservableCollection<BasePodcast>();
            for (int i = 0; i < results.Count; i++)
            {
                listboxItems.Add(results[i]);
            }
            SearchResults = listboxItems;
        }
    }
}
