using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneSale.Elements
{
    class Category
    {
        //TODO :
        public Category(string num, string caption)
        {
            this.num = num;
            this.caption = caption;
            for (int i = 5; i != 0; i--)
            {
                if (num[i - 1] != '0')
                {
                    CategoryGrade = Enum.Parse<CategoryGrade>(Enum.GetName(typeof(CategoryGrade), i));
                    return;
                }
            }
            CategoryGrade = CategoryGrade.MainRoot;
        }
        public static ObservableCollection<Category> GetCategories(SqlDataReader reader)
        {
            ObservableCollection<Category> categories = new ObservableCollection<Category>();
            while (reader.Read())
            {
                try
                {
                    categories.Add(new Category(reader[0].ToString(), reader[1].ToString()));
                }
                catch
                {

                }
            }
            return categories;
        }

        private Category()
        {

        }
        public static Category MainRoot = new Category() { Num = "00000", Caption = "MainRoot", CategoryGrade = CategoryGrade.MainRoot, Root = MainRoot, Leaves = new List<Category>() };
        string num, caption;
        Category root;
        List<Category> leaves;
        public CategoryGrade CategoryGrade;

        public string Num { get => num; set => num = value; }
        public string Caption { get => caption; set => caption = value; }
        internal Category Root { get => root; set => root = value; }
        internal List<Category> Leaves { get => leaves; set => leaves = value; }
    }
}
