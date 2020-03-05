using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Podcaster
{
    public class SearchVM : ObservableObject
    {

        private ObservableCollection<SearchDisplay> _SearchResults;
        public ObservableCollection<SearchDisplay> SearchResults
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

        public SearchVM()
        {

        }

        public void SearchAddButton_Click(object sender, RoutedEventArgs e)
        {

        }


        public void SearchTextBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            List<SearchResult> results = Itunes.SearchPodcast(sender.Text);
            ObservableCollection<SearchDisplay> listboxItems = new ObservableCollection<SearchDisplay>();
            for (int i = 0; i < results.Count; i++)
            {
                listboxItems.Add(new SearchDisplay(results[i]));
            }
            SearchResults = listboxItems;
        }
    }
}
