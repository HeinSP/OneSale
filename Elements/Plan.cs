using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneSale.Elements
{
    class Plan
    {
        //TODO :

        long id;
        Dictionary<Link, int> deals;
        double originPrice;
        double finalPrice;
        double totalRecall;
        DateTime date;
        Link mainLink;
    }
}
