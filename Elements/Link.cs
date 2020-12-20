using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneSale.Elements
{
    class Link
    {
        //TODO :
        public Link()
        {

        }
        long num;
        string caption;
        Product product;
        Quality quality;
        Unit unit;
        double amount;
        Price price;
        Shop shop;
        string brand;
        string series;
        DateTime date;
        double freight;
        string linkAddress;
        string fullCaption;

        public long Num { get => num; set => num = value; }
        internal Product Product { get => product; set => product = value; }
        internal Quality Quality { get => quality; set => quality = value; }
        internal Unit Unit { get => unit; set => unit = value; }
        public double Amount { get => amount; set => amount = value; }
        internal Price Price { get => price; set => price = value; }
        internal Shop Shop { get => shop; set => shop = value; }
        public string Brand { get => brand; set => brand = value; }
        public string Series { get => series; set => series = value; }
        public DateTime Date { get => date; set => date = value; }
        public double Freight { get => freight; set => freight = value; }
        public string LinkAddress { get => linkAddress; set => linkAddress = value; }
        public string FullCaption { get => fullCaption; set => fullCaption = value; }
        public string Caption { get => caption; set => caption = value; }
    }
}
