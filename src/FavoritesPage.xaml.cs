using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Podcaster
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FavoritesPage : Page
    {
        private FavoritesVM VMContext;
        public FavoritesPage()
        {
            this.InitializeComponent();
            VMContext = new FavoritesVM();
            DataContext = VMContext;
        }

        private void FavoritesListBox_Play_Click(object sender, RoutedEventArgs e)
        {
            VMContext.FavoritesListBox_Play(sender, e);
        }

        private void FavoritesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VMContext.FavoritesList_SelectionChanged(sender, e);
            EpisodeList.ItemsSource = VMContext.CurrentPodcastEpisodes;
        }
    }
}
