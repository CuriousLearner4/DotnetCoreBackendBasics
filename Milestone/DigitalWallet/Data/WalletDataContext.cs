using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalWallet.Models;

namespace DigitalWallet.Data
{
    public class WalletDataContext
    {
        public List<User> users;
        public WalletDataContext()
        {
            users = new List<User>();
        }

    }
}
