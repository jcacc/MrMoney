using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMoney.Domain.Interfaces
{
    public interface IUserAccountActions
    {
        void CheckBalance();
        void PlaceDeposit();
        void MakeWithdrawal();
        
    }
}
