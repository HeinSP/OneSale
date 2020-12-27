using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneSale.Elements
{
    class Category : Element
    {
        public Category(SqlDataReader reader) : this(reader[0].ToString(), reader[1].ToString()) { }
        public Category(string numString, string caption)
        {
            GetThis(numString, caption);
        }
        public Category() { }
        public override T Load<T>(SqlDataReader reader)
        {
            return GetThis(reader[0].ToString(), reader[1].ToString()) as T;
        }
        private Category GetThis(string numString, string caption)
        {
            this.numString = numString;
            this.caption = caption;
            this.num = GetNum(this.numString);
            for (int i = 5; i != 0; i--)
            {
                if (numString[i - 1] != '0')
                {
                    CategoryGrade = Enum.Parse<CategoryGrade>(Enum.GetName(typeof(CategoryGrade), i));
                    return this;
                }
            }
            CategoryGrade = CategoryGrade.MainRoot;
            return this;
        }
        public static long GetNum(string numString)
        {
            long result = 0;
            for (int i = 0; i != 5; i++)
            {
                result = result * 36 + Universal.Encryption.Scale36ToInt(numString[i]);
            }
            return result;
        }
        public static string GetNumString(int num)
        {
            string result = "";
            for (int i = 0; i != 5; i++)
            {
                result = Universal.Encryption.IntToScale36(num % 36) + result;
                num /= 36;
            }
            return result;
        }
        public static Category MainRoot = new Category() { NumString = "00000", Caption = "MainRoot", CategoryGrade = CategoryGrade.MainRoot, Root = MainRoot, Leaves = new List<Category>() };
        string numString;
        Category root;
        List<Category> leaves;
        public CategoryGrade CategoryGrade;

        public string NumString { get => numString; set => numString = value; }
        internal Category Root { get => root; set => root = value; }
        internal List<Category> Leaves { get => leaves; set => leaves = value; }
    }
}
