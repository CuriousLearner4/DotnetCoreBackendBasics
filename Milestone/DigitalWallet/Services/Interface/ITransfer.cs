using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Services.Interface
{
    public interface ITransfer
    {
        public void Transfer(int from, int to, decimal amount);
    }
}
