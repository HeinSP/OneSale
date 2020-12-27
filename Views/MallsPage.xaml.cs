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
using OneSale.Elements;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace OneSale.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MallsPage : Page
    {
        public MallsPage()
        {
            this.InitializeComponent();
            RefreshMalls();
        }
        internal static void RefreshMalls()
        {
            MainPage.Current.Malls.Clear();
            SelectLib mallLib = new SelectLib("mallLib");
            mallLib.GetReader("Malls", "");
            MainPage.Current.Malls.Refresh(mallLib.DbReader);
        }
    }
}
