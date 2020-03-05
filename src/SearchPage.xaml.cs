using System;
using System.Collections.Generic;
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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Podcaster
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchPage : Page
    {
        private SearchVM VMContext;
        public SearchPage()
        {
            this.InitializeComponent();
            VMContext = new SearchVM();
            DataContext = VMContext;
        }

        private void SearchAddButton_Click(object sender, RoutedEventArgs e)
        {
            VMContext.SearchAddButton_Click(sender, e);
        }

        private void SearchTextBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            VMContext.SearchTextBox_QuerySubmitted(sender, args);
        }
    }
}
