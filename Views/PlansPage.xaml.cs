using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace OneSale.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class PlansPage : Page
    {
        public PlansPage()
        {
            this.InitializeComponent();/*
            Lib.Spider spider = new Lib.Spider();
            string title = spider.GetInformation("https://mall.jd.com/index-1000005001.html");
            title = title.Trim();
            title = title.Remove(title.Length - 5);
            MainPage.Current.LibInfoBar.Message = "====" + title + "====";*/
        }
        int done = 0;
        int amount;
        string result;
        private async void GetInformation()
        {
            for (int i = 0; i != amount; i++)
            {
                Thread.Sleep(200);
                result += "Testing ... Circling : " + done + "\n";
                done++;
            }
            //btnClear.IsEnabled = true;
            return ;
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            done = 0;
            tbxResult.Text = "";
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            btnClear.IsEnabled = false;
            amount = int.Parse(tbxAmount.Text);
            Task.Run(GetInformation);
        }
    }
}
