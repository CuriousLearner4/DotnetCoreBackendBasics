using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalWallet.Models;

namespace DigitalWallet.Services.Interface
{
    public interface IWalletService: ICreate,ITransfer,IAccountStatement,IOverview
    {
        
        
        
        
    }
}
