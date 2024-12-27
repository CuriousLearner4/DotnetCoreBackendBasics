using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Services.Interface
{
    public interface ICreate
    {
        public void Create(string name, decimal amount);
    }
}
