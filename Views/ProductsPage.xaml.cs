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
using OneSale.Elements;
using Universal.SqlSOperation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace OneSale.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ProductsPage : Page
    {
        public ProductsPage()
        {
            this.InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            SelectLib productLib = new SelectLib("productLib");
            productLib.GetReader("Products", "");
            products.Refresh(productLib.DbReader);
            productRepeater.ElementPrepared += ProductRepeater_ElementPrepared;
        }
        private void InjectToProducts()
        {
            int c = products.Count;
            for (int i = 0; i != 100; i++)
            {
                products.Add(new Product(i + c, "Product " + (i + c)));
            }
        }
        int preparedItems;

        ObservableElements<Product> products = new ObservableElements<Product>();

        private void ProductRepeater_ElementPrepared(Microsoft.UI.Xaml.Controls.ItemsRepeater sender, Microsoft.UI.Xaml.Controls.ItemsRepeaterElementPreparedEventArgs args)
        {
            StackPanel stackPanel = args.Element as StackPanel;
            preparedItems = int.Parse(stackPanel.Tag.ToString());
        }
        private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (preparedItems >= 2000)
            {
                products.Clear();
            }
            if (preparedItems >= products.Count - 21)
            {
                InjectToProducts();
            }
            MainPage.Current.LibInfoBar.Message = preparedItems + " prepared ... Total Items : " + products.Count;
        }
    }
}
