using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneSale.Elements
{
    enum Categories : int
    {
        AllGoods
    }
    enum Quality : int
    {
        Bad,
        Medium,
        Good,
        High,
        Marvelous
    }
    enum Unit : int
    {
        Others,
        kg,
        L, 
        m3, 
        m
    }

    enum Mall : int
    {
        None,
        Taobao,
        JD,
        PDD,
        Suning,
        Xiaomi,
        Youpin,
        Yanxuan
    }
    enum ShopGrade : int
    {
        A,
        B,
        C,
        D,
        E
    }
    class Const
    {
    }
}
