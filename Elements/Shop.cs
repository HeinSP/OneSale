using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneSale.Elements
{
    class Shop
    {
        //TODO :

        public Shop(Mall mall, ShopGrade shopGrade)
        {
            this.Mall = mall;
            this.shopGrade = shopGrade;
        }

        public int CountLinks()
        {
            return 0;
        }
        int num;
        string name;
        Mall mall;
        ShopGrade shopGrade = ShopGrade.A;

        internal Mall Mall { get => mall; set => mall = value; }
        internal ShopGrade ShopGrade { get => shopGrade; set => shopGrade = value; }
        public string Name { get => name; set => name = value; }
    }
}
