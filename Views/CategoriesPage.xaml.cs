using OneSale.Elements;
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
using Windows.UI.Xaml.Navigation;
using Universal.SqlSOperation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace OneSale.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class CategoriesPage : Page
    {
        public CategoriesPage()
        {
            this.InitializeComponent();
            Current = this;
            RefreshCategories();
        }
        internal static void RefreshCategories()
        {
            MainPage.Current.Categories.Clear();
            MainPage.Current.First.Clear();
            MainPage.Current.Second.Clear();
            MainPage.Current.Third.Clear();
            MainPage.Current.Special.Clear();
            SelectLib categoryLib = new SelectLib("categoryLib");
            categoryLib.GetReader("Categories", "");
            MainPage.Current.Categories.Refresh(categoryLib.DbReader);
            foreach (Category category in MainPage.Current.Categories)
            {
                switch (category.CategoryGrade)
                {
                    case CategoryGrade.First:
                        {
                            MainPage.Current.First.Add(category);
                            break;
                        }
                }
            }
        }
        internal static CategoriesPage Current;
        
        private void abtnMirroredView_Click(object sender, RoutedEventArgs e)
        {
            //rightGrid.Width = new GridLength(1, GridUnitType.Star);
            contentGrid.IsPaneOpen = true;
        }

        private void abtnNormalView_Click(object sender, RoutedEventArgs e)
        {
            //rightGrid.Width = GridLength.Auto;
            contentGrid.IsPaneOpen = false;
        }

        private void contentGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            contentGrid.OpenPaneLength = contentGrid.ActualWidth / 2;
        }

        private void btnCategory_Click(object sender, RoutedEventArgs e)
        {
            string caption = (sender as Button).Content.ToString();
            Category categorySelected = MainPage.Current.Categories.Single(check => check.Caption == caption);
            switch (categorySelected.CategoryGrade)
            {
                case CategoryGrade.First:
                    {
                        MainPage.Current.Second.Clear();
                        MainPage.Current.Third.Clear();
                        MainPage.Current.Special.Clear();
                        foreach (Category c in MainPage.Current.Categories)
                        {
                            if (c.NumString[0] == categorySelected.NumString[0] && c.CategoryGrade == CategoryGrade.Second)
                            {
                                MainPage.Current.Second.Add(c);
                            }
                        }
                        break;
                    }
                case CategoryGrade.Second:
                    {
                        MainPage.Current.Third.Clear();
                        MainPage.Current.Special.Clear();
                        foreach (Category c in MainPage.Current.Categories)
                        {
                            if (c.NumString.Remove(2) == categorySelected.NumString.Remove(2) && c.CategoryGrade == CategoryGrade.Third)
                            {
                                MainPage.Current.Third.Add(c);
                            }
                        }
                        break;
                    }
                case CategoryGrade.Third:
                    {
                        MainPage.Current.Special.Clear();
                        foreach (Category c in MainPage.Current.Categories)
                        {
                            if (c.NumString.Remove(3) == categorySelected.NumString.Remove(3) && c.CategoryGrade == CategoryGrade.Special)
                            {
                                MainPage.Current.Special.Add(c);
                            }
                        }
                        break;
                    }
            }
        }
    }
}
