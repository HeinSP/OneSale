using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneSale.Elements
{
    class Product : Element
    {
        public Product(int num, string caption)
        {
            this.num = num;
            this.caption = caption;

        }
        public Product() { }
        public Product(SqlDataReader reader)
        {
            GetThis(reader);
        }
        public override T Load<T>(SqlDataReader reader)
        {
            return GetThis(reader) as T;
        }
        private Product GetThis(SqlDataReader reader)
        {
            this.num = long.Parse(reader[0].ToString());
            caption = reader[1].ToString();
            Category category = new Category();
            category = MainPage.Current.Categories[Category.GetNum(reader[2].ToString())];
            firstCategory = MainPage.Current.Categories.Single(check => check.NumString == (category.NumString.Substring(0, 1) + "0000"));
            secondCategory = MainPage.Current.Categories.Single(check => check.NumString == (category.NumString.Substring(0, 2) + "000"));
            thirdCategory = MainPage.Current.Categories.Single(check => check.NumString == (category.NumString.Substring(0, 3) + "00"));
            try
            {
                specialCategory = MainPage.Current.Categories.Single(
                check => check.NumString == (category.NumString.Substring(0, 4) + "0") && check.CategoryGrade == CategoryGrade.Special);
            }
            catch { }
            return this;
        }
        public List<Link> GetLinks()
        {
            //TODO :
            return null;
        }
        public List<Plan> GetPlans()
        {
            //TODO :
            return null;
        }
        Category firstCategory;
        Category secondCategory;
        Category thirdCategory;
        Category specialCategory;

        internal Category FirstCategory { get => firstCategory; set => firstCategory = value; }
        internal Category SecondCategory { get => secondCategory; set => secondCategory = value; }
        internal Category ThirdCategory { get => thirdCategory; set => thirdCategory = value; }
        internal Category SpecialCategory { get => specialCategory; set => specialCategory = value; }

        //Features features;
    }
}
