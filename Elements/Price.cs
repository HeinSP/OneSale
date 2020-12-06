using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneSale.Elements
{
    class Price
    {
        //TODO :

        double origin;  // 原价，初始价
        double discount;  // 直减优惠
        List<WhenEnough> shopDiscountWhenEnough, mallDiscountWhenEnough, shopDiscountWhenDeals, mallDiscountWhenDeals;  // 店铺、商城满减优惠、品类券、购物券
        List<WhenEnough> shopEveryDiscountWhenEnough, mallEveryDiscountWhenEnough;  // 店铺、商城每满Enough减Discount
        double TaoGold;  // 淘金币
        double mallVIP;  // 商城会员：88 VIP， JD PLUS
        WhenEnough deposit;  // 定金与定金立减
        double recall;  // 收返
    }
    struct WhenEnough
    {
        internal WhenEnough(double enough, double discount, bool isPercentage)
        {
            Enough = enough;
            Discount = discount;
            IsPercentage = isPercentage;
        }
        double Enough;
        double Discount;
        bool IsPercentage;
    }
}
