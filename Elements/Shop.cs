﻿using System;
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

        Mall mall = Mall.None;
        ShopGrade shopGrade = ShopGrade.A;

        internal Mall Mall { get => mall; set => mall = value; }
        internal ShopGrade ShopGrade { get => shopGrade; set => shopGrade = value; }
    }
}
