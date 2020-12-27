using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneSale.Elements
{
    class Shop : Element
    {
        public Shop() { }
        public Shop(int num, string caption)
        {
            this.num = num;
            this.caption = caption;
            mall = new Mall();
            shopGrade = ShopGrade.A;
        }
        public Shop(SqlDataReader reader)
        {
            GetThis(reader);
        }
        public override T Load<T>(SqlDataReader reader)
        {
            return GetThis(reader) as T;
        }
        private Shop GetThis(SqlDataReader reader)
        {
            this.num = long.Parse(reader[0].ToString());
            caption = reader[1].ToString();
            mall = MainPage.Current.Malls.Find(int.Parse(reader[2].ToString()));
            shopGrade = (ShopGrade)int.Parse(reader[3].ToString());
            return this;
        }
        public int CountLinks()
        {
            return 0;
        }
        Mall mall;
        ShopGrade shopGrade = ShopGrade.A;

        internal Mall Mall { get => mall; set => mall = value; }
        internal ShopGrade ShopGrade { get => shopGrade; set => shopGrade = value; }
    }
}
