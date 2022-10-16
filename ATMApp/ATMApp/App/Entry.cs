using MrMoney.UI;
using MrMoney;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMoney.App
{
     class Entry
    {
        static void Main(string[] args)
        {
            
            MrMoney MrMoney = new MrMoney();
            MrMoney.InitializeData();
            MrMoney.Run();
    

        }
    }
}
