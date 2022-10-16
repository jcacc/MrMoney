using ATMApp.UI;
using ATMApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.App
{
     class Entry
    {
        static void Main(string[] args)
        {
            
            ATMApp atmAPP = new ATMApp();
            atmAPP.InitializeData();
            atmAPP.Run();
    

        }
    }
}
