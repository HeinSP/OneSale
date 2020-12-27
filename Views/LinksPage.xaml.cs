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
using System.Collections;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Hosting;
using Windows.UI;
using System.Reflection;
using OneSale.Elements;
using Universal.SqlSOperation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace OneSale.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class LinksPage : Page
    {
        public LinksPage()
        {
            this.InitializeComponent();
            InitializeData();
        }

        public MyItemsSource filteredRecipeData = new MyItemsSource(null);
        public List<Recipe> staticRecipeData;

        private void InitializeData()
        {
            List<NestedCategory> nestedCategories = new List<NestedCategory>
            {
                new NestedCategory("Fruits", GetFruits()),
                new NestedCategory("Vegetables", GetVegetables()),
                new NestedCategory("Grains", GetGrains()),
                new NestedCategory("Proteins", GetProteins())
            };

            // Initialize list of colors for animatedScrollRepeater
            VariedImageSizeRepeater.ElementPrepared += OnElementPrepared;
            // Initialize custom MyItemsSource object with new recipe data
            List<Recipe> RecipeList = GetRecipeList();
            filteredRecipeData.InitializeCollection(RecipeList);
            // Save a static copy to compare to while filtering
            staticRecipeData = RecipeList;
            VariedImageSizeRepeater.ItemsSource = filteredRecipeData;
        }

        private ObservableCollection<string> GetFruits()
        {
            return new ObservableCollection<string> { "Apricots", "Bananas", "Grapes", "Strawberries", "Watermelon", "Plums", "Blueberries" };
        }

        private ObservableCollection<string> GetVegetables()
        {
            return new ObservableCollection<string> { "Broccoli", "Spinach", "Sweet potato", "Cauliflower", "Onion", "Brussels sprouts", "Carrots" };
        }
        private ObservableCollection<string> GetGrains()
        {
            return new ObservableCollection<string> { "Rice", "Quinoa", "Pasta", "Bread", "Farro", "Oats", "Barley" };
        }
        private ObservableCollection<string> GetProteins()
        {
            return new ObservableCollection<string> { "Steak", "Chicken", "Tofu", "Salmon", "Pork", "Chickpeas", "Eggs" };
        }

        private IList<string> GetColors()
        {
            // Initialize list of colors for animated scrolling sample
            IList<string> colors = (typeof(Colors).GetRuntimeProperties().Select(c => c.ToString())).ToList();
            for (int i = 0; i < colors.Count(); i++)
            {
                colors[i] = colors[i].Substring(17);

            }

            return colors;

        }

        private List<Recipe> GetRecipeList()
        {
            // Initialize list of recipes for varied image size layout sample
            var rnd = new Random();
            List<Recipe> tempList = new List<Recipe>(
                                        Enumerable.Range(0, 100).Select(k =>
                                            new Recipe
                                            {
                                                Num = k,
                                                Name = "Recipe " + k.ToString(),
                                                Color = GetColors()[(k % 100) + 1]
                                            }));
            foreach (Recipe rec in tempList)
            {
                // Add one food from each option into the recipe's ingredient list and ingredient string
                string fruitOption = GetFruits()[rnd.Next(0, 6)];
                string vegOption = GetVegetables()[rnd.Next(0, 6)];
                string grainOption = GetGrains()[rnd.Next(0, 6)];
                string proteinOption = GetProteins()[rnd.Next(0, 6)];
                rec.Ingredients = "\n" + fruitOption + "\n" + vegOption + "\n" + grainOption + "\n" + proteinOption;
                rec.IngList = new List<string>() { fruitOption, vegOption, grainOption, proteinOption };

                // Add extra ingredients so items have varied heights in the layout
                rec.RandomizeIngredients();
                //filteredRecipeData.Add(rec);
            }

            return tempList;
        }

        private void cbxFirst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string caption = cbxFirst.SelectedItem.ToString();
            Category categorySelected = MainPage.Current.Categories.Single(check => check.Caption == caption);
            MainPage.Current.Second.Clear();
            MainPage.Current.Third.Clear();
            MainPage.Current.Special.Clear();
            cbxSecond.Items.Clear();
            cbxThird.Items.Clear();
            foreach (Category c in MainPage.Current.Categories)
            {
                if (c.NumString[0] == categorySelected.NumString[0] && c.CategoryGrade == CategoryGrade.Second)
                {
                    MainPage.Current.Second.Add(c);
                    cbxSecond.Items.Add(c.Caption);
                }
            }
        }

        private void cbxSecond_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxSecond.Items.Count == 0)
            {
                return;
            }
            string caption = cbxSecond.SelectedItem.ToString();
            Category categorySelected = MainPage.Current.Categories.Single(check => check.Caption == caption);
            MainPage.Current.Third.Clear();
            MainPage.Current.Special.Clear();
            cbxThird.Items.Clear();
            foreach (Category c in MainPage.Current.Categories)
            {
                if (c.NumString.Remove(2) == categorySelected.NumString.Remove(2) && c.CategoryGrade == CategoryGrade.Third)
                {
                    MainPage.Current.Third.Add(c);
                    cbxThird.Items.Add(c.Caption);
                }
            }
        }

        private void cbxThird_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void StackPanel_Tapped(object sender, RoutedEventArgs e)
        {
            StackPanel stackPanel = sender as StackPanel;
            MainPage.Current.LibInfoBar.Message = stackPanel.Tag + " loaded.";
        }
        private void OnElementPrepared(Microsoft.UI.Xaml.Controls.ItemsRepeater sender, Microsoft.UI.Xaml.Controls.ItemsRepeaterElementPreparedEventArgs args)
        {
            var item = ElementCompositionPreview.GetElementVisual(args.Element);
            var svVisual = ElementCompositionPreview.GetElementVisual(Animated_ScrollViewer);
            var scrollProperties = ElementCompositionPreview.GetScrollViewerManipulationPropertySet(Animated_ScrollViewer);
            StackPanel stackPanel = args.Element as StackPanel;
            int preparedItems = int.Parse(stackPanel.Tag.ToString().Substring(7));
            if (preparedItems >= staticRecipeData.Count - 1)
            {
                staticRecipeData.AddRange(GetRecipeList());
                //filteredRecipeData.AddToCollection(GetRecipeList());
            }
            MainPage.Current.LibInfoBar.Message = stackPanel.Tag + " prepared ... Total Items : " + filteredRecipeData.Count;
            var scaleExpresion = scrollProperties.Compositor.CreateExpressionAnimation();
            scaleExpresion.SetReferenceParameter("svVisual", svVisual);
            scaleExpresion.SetReferenceParameter("scrollProperties", scrollProperties);
            scaleExpresion.SetReferenceParameter("item", item);

            // Scale the item based on the distance of the item relative to the center of the viewport.
            scaleExpresion.Expression = "1 - abs((svVisual.Size.Y/2 - scrollProperties.Translation.Y) - (item.Offset.Y + item.Size.Y/2))*(.25/(svVisual.Size.Y/2))";

            // Animate the item to change size based on distance from center of viewpoint
            item.StartAnimation("Scale.X", scaleExpresion);
            item.StartAnimation("Scale.Y", scaleExpresion);
            var centerPointExpression = scrollProperties.Compositor.CreateExpressionAnimation();
            centerPointExpression.SetReferenceParameter("item", item);
            centerPointExpression.Expression = "Vector3(item.Size.X/2, item.Size.Y/2, 0)";
            item.StartAnimation("CenterPoint", centerPointExpression);
        }
    }

    public class NestedCategory
    {
        public string CategoryName { get; set; }
        public ObservableCollection<string> CategoryItems { get; set; }
        public NestedCategory(string catName, ObservableCollection<string> catItems)
        {
            CategoryName = catName;
            CategoryItems = catItems;
        }
    }

    public class StringOrIntTemplateSelector : DataTemplateSelector
    {
        // Define the (currently empty) data templates to return
        // These will be "filled-in" in the XAML code.
        public DataTemplate StringTemplate { get; set; }

        public DataTemplate IntTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            // Return the correct data template based on the item's type.
            if (item.GetType() == typeof(string))
            {
                return StringTemplate;
            }
            else if (item.GetType() == typeof(int))
            {
                return IntTemplate;
            }
            else
            {
                return null;
            }
        }
    }

    public class Recipe
    {
        public int Num { get; set; }
        public string Ingredients { get; set; }
        public List<string> IngList { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int NumIngredients
        {
            get
            {
                return IngList.Count();
            }
        }

        public void RandomizeIngredients()
        {
            // To give the items different heights, give recipes random numbers of random ingredients
            Random rndNum = new Random();
            Random rndIng = new Random();

            ObservableCollection<string> extras = new ObservableCollection<string>{
                                                         "Garlic",
                                                         "Lemon",
                                                         "Butter",
                                                         "Lime",
                                                         "Feta Cheese",
                                                         "Parmesan Cheese",
                                                         "Breadcrumbs"};
            for (int i = 0; i < rndNum.Next(0, 4); i++)
            {
                string newIng = extras[rndIng.Next(0, 6)];
                if (!IngList.Contains(newIng))
                {
                    Ingredients += "\n" + newIng;
                    IngList.Add(newIng);
                }
            }

        }
    }

    // Custom data source class that assigns elements unique IDs, making filtering easier
    public class MyItemsSource : IList, Microsoft.UI.Xaml.Controls.IKeyIndexMapping, INotifyCollectionChanged
    {
        private List<Recipe> inner = new List<Recipe>();

        public MyItemsSource(IEnumerable<Recipe> collection)
        {
            InitializeCollection(collection);
        }

        public void InitializeCollection(IEnumerable<Recipe> collection)
        {
            inner.Clear();
            if (collection != null)
            {
                inner.AddRange(collection);
            }

            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
        public void AddToCollection(IEnumerable<Recipe> collection)
        {
            //inner.Clear();
            if (collection != null)
            {
                inner.AddRange(collection);
            }

            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        #region IReadOnlyList<T>
        public int Count => this.inner != null ? this.inner.Count : 0;

        public object this[int index]
        {
            get
            {
                return inner[index] as Recipe;
            }

            set
            {
                inner[index] = (Recipe)value;
            }
        }

        public IEnumerator<Recipe> GetEnumerator() => this.inner.GetEnumerator();

        #endregion

        #region INotifyCollectionChanged
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion


        // TODO: 通过序号或key（主键）检索，待重写此功能后删除
        #region IKeyIndexMapping
        public string KeyFromIndex(int index)
        {
            return inner[index].Num.ToString();
        }

        public int IndexFromKey(string key)
        {
            foreach (Recipe item in inner)
            {
                if (item.Num.ToString() == key)
                {
                    return inner.IndexOf(item);
                }
            }
            return -1;
        }

        #endregion

        #region Unused List methods
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int Add(object value)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(object value)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(object value)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        public void Remove(object value)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public bool IsFixedSize => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public bool IsSynchronized => throw new NotImplementedException();

        public object SyncRoot => throw new NotImplementedException();

        #endregion
    }
}
