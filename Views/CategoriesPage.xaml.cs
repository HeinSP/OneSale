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
            Initialize();
        }
        private void Initialize()
        {
            categories.Clear();
            first.Clear();
            second.Clear();
            third.Clear();
            special.Clear();
            SelectLib categoryLib = new SelectLib("categoryLib");
            categoryLib.GetReader("Categories", "");
            categories = Category.GetCategories(categoryLib.DbReader);
            foreach (Category category in categories)
            {
                switch (category.CategoryGrade)
                {
                    case CategoryGrade.First:
                        {
                            first.Add(category);
                            break;
                        }
                }
            }
        }
        internal static CategoriesPage Current;
        ObservableCollection<Category> categories = new ObservableCollection<Category>();
        ObservableCollection<Category> first = new ObservableCollection<Category>();
        ObservableCollection<Category> second = new ObservableCollection<Category>();
        ObservableCollection<Category> third = new ObservableCollection<Category>();
        ObservableCollection<Category> special = new ObservableCollection<Category>();
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
            string num = (sender as Button).Tag.ToString();
            Category categorySelected = new Category(num, categories.Single(check => check.Num == num).Caption);
            switch (categorySelected.CategoryGrade)
            {
                case CategoryGrade.First:
                    {
                        second.Clear();
                        third.Clear();
                        special.Clear();
                        foreach (Category c in categories)
                        {
                            if (c.Num[0] == categorySelected.Num[0] && c.CategoryGrade == CategoryGrade.Second)
                            {
                                second.Add(c);
                            }
                        }
                        break;
                    }
                case CategoryGrade.Second:
                    {
                        third.Clear();
                        special.Clear();
                        foreach (Category c in categories)
                        {
                            if (c.Num.Remove(2) == categorySelected.Num.Remove(2) && c.CategoryGrade == CategoryGrade.Third)
                            {
                                third.Add(c);
                            }
                        }
                        break;
                    }
                case CategoryGrade.Third:
                    {
                        special.Clear();
                        foreach (Category c in categories)
                        {
                            if (c.Num.Remove(3) == categorySelected.Num.Remove(3) && c.CategoryGrade == CategoryGrade.Special)
                            {
                                special.Add(c);
                            }
                        }
                        break;
                    }
            }
        }
    }
}
