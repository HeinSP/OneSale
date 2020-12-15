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

            try
            {
                string libInfo = dataLib.Connect();
                LibInfoBar.Message = libInfo;
                if (libInfo == Lib.DataLib.SuccessfulInfo)
                {
                    //LibInfoBar.IconSource = new Microsoft.UI.Xaml.Controls.FontIconSource() {FontFamily = new FontFamily("Segoe MDL2 Assets"), Glyph = "\xE10B" };
                    LibInfoBar.Severity = Microsoft.UI.Xaml.Controls.InfoBarSeverity.Success;
                }
                else
                {
                    LibInfoBar.Severity = Microsoft.UI.Xaml.Controls.InfoBarSeverity.Error;
                }
            }
            catch
            {
                
            }
        }
        private void Initialize()
        {
            
        }
        internal Lib.DataLib dataLib = new Lib.DataLib();

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            splitView.Height = ActualHeight - commandBar.ActualHeight;
            tbkInfo.Text = "Page Size : " + this.ActualSize.ToString();
        }

        private void NavigationView_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            /*
             * if (args.IsSettingsSelected)
            {
                contentFrame.Navigate(typeof(SampleSettingsPage));
            }
            else
            {
                var selectedItem = (Microsoft.UI.Xaml.Controls.NavigationViewItem)args.SelectedItem;
                if (selectedItem != null)
                {
                    string selectedItemTag = ((string)selectedItem.Tag);
                    sender.Header = "Sample Page " + selectedItemTag.Substring(selectedItemTag.Length - 1);
                    string pageName = "AppUIBasics.SamplePages." + selectedItemTag;
                    Type pageType = Type.GetType(pageName);
                    contentFrame.Navigate(pageType);
                }
            }
            */
        }
    }
}
