using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

using System.Xml;
using System.Xml.Serialization;
using System.ServiceModel;
using Windows.Web.Syndication;
using System.Net;
using Windows.Storage;
using Windows.Media.Core;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Podcaster
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<SearchDisplay> Favorites = new ObservableCollection<SearchDisplay>();
        private ItunesAPI Itunes;
        private SearchPage SearchView;
        private FavoritesPage FavoritesView = new FavoritesPage();
        public MainPage()
        {
            this.InitializeComponent();

            SearchView = new SearchPage();
            SearchView.DataContext = new SearchVM();
            FavoritesView = new FavoritesPage();
            FavoritesView.DataContext = new FavoritesVM();
        }

        private void SearchButton_Click(object sender, AutoSuggestBoxQuerySubmittedEventArgs e)
        {
            List<SearchResult> results = Itunes.SearchPodcast(SearchTextBox.Text);
            ObservableCollection<SearchDisplay> listboxItems = new ObservableCollection<SearchDisplay>();
            for (int i = 0; i < results.Count; i++)
            {
                listboxItems.Add(new SearchDisplay(results[i]));
            }
            SearchListBox.ItemsSource = listboxItems;
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


        private void SearchAddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                if (button.DataContext.GetType() == typeof(SearchDisplay))
                {
                    FavoritesListBox.Items.Add((SearchDisplay)button.DataContext);
                }
            }
            catch { }
        }
    }
    public class SearchDisplay
    {
        public string PodcastString { get; set; }
        public BitmapImage AlbumArt { get; set; }
        public string FeedURL { get; set; }
        public SearchDisplay(SearchResult result)
        {
            AlbumArt = new BitmapImage();
            AlbumArt.UriSource = new Uri(result.artworkUrl100);
            PodcastString = result.collectionName + " - " + result.artistName;
            FeedURL = result.feedUrl;
        }
    }
}