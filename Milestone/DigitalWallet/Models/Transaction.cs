using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Models
{
    public class Transaction
    {
        public int Id {  get; set; }

        public int FromAccount { get; set; }

        public int ToAccount { get; set; }

        public decimal Amount { get; set; }

        public override string ToString()
        {
            string Txn = $"Transaction Id : {Id} from account id : {FromAccount} to account id : {ToAccount} of amount Rs{Amount}";
            return Txn;
        }

    }
}
