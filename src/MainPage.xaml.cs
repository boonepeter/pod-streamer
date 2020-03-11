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
        private SearchPage SearchView;
        private FavoritesPage FavoritesView;
        public MainPage()
        {
            this.InitializeComponent();

            SearchView = new SearchPage(this);
            FavoritesView = new FavoritesPage(this);
            ContentFrame.Content = FavoritesView;
            ((SearchVM)SearchView.DataContext).FavVM = (FavoritesVM)FavoritesView.DataContext;
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            switch (args.InvokedItemContainer.Content.ToString())
            {
                case "Favorites":
                    ContentFrame.Content = FavoritesView;
                    break;
                case "Search":
                    ContentFrame.Content = SearchView;
                    break;
            }

        }

        public void PlayFromSource(Uri source)
        {
            PlayBar.Source = MediaSource.CreateFromUri(source);
        }

    }
    public class SearchDisplay
    {
        public string PodcastString { get; set; }
        public BitmapImage AlbumArt { get; set; }
        public string FeedURL { get; set; }
        public SearchDisplay(BasePodcast result)
        {
            AlbumArt = new BitmapImage();
            AlbumArt.UriSource = new Uri(result.artworkUrl100);
            PodcastString = result.collectionName + " - " + result.artistName;
            FeedURL = result.feedUrl;
        }
    }


}