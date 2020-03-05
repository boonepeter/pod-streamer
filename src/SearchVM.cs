using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private ItunesAPI Itunes = new ItunesAPI();

        public SearchVM()
        {

        }


    }
}
