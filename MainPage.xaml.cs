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
using Universal.SqlSOperation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace OneSale
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            InitializeDataLib();
            Initialize();
            
        }
        private void InitializeDataLib()
        {
            string server = "127.0.0.1", database = "Ceres", userID = "sa", password = "0506Hein";
            try
            {
                DataLib.InitializeConnection(server, database, userID, password);
                LibInfoBar.Message = "DataLib connected successfully.";
                LibInfoBar.Severity = Microsoft.UI.Xaml.Controls.InfoBarSeverity.Success;
            }
            catch (Exception e)
            {
                LibInfoBar.Message = e.Message;
            }
        }
        private void Initialize()
        {
            navigationView.SelectedItem = navigationView.MenuItems.OfType<Microsoft.UI.Xaml.Controls.NavigationViewItem>().First();
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //splitView.Height = ActualHeight - commandBar.ActualHeight;
            tbkInfo.Text = "Page Size : " + this.ActualSize.ToString();
        }
        private void NviAdd_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }
        private void NavigationView_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                navigationFrame.Navigate(typeof(Views.SettingsPage));
            }
            else
            {
                var selectedItem = (Microsoft.UI.Xaml.Controls.NavigationViewItem)args.SelectedItem;
                if (selectedItem != null)
                {
                    string selectedItemTag = ((string)selectedItem.Tag);
                    sender.Header = selectedItemTag;
                    string pageName = "OneSale.Views." + selectedItemTag + "Page";
                    Type pageType = Type.GetType(pageName);
                    navigationFrame.Navigate(pageType);
                }
            }
        }

        private void NavigationViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
    }
}
