using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneSale.Elements
{
    class Product
    {
        //TODO :

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
        string caption;
        long id;
        Category rootCategory;
        Category secondCategory;
        Category thridCategory;
        Category specialCategory;

        public string Caption { get => caption; set => caption = value; }
        public long Id { get => id; set => id = value; }
        internal Category RootCategory { get => rootCategory; set => rootCategory = value; }
        internal Category SecondCategory { get => secondCategory; set => secondCategory = value; }
        internal Category ThridCategory { get => thridCategory; set => thridCategory = value; }
        internal Category SpecialCategory { get => specialCategory; set => specialCategory = value; }

        //Features features;
    }
}
