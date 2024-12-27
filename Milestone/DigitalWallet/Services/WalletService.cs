using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalWallet.Data;
using DigitalWallet.Models;
using DigitalWallet.Services.Interface;

namespace DigitalWallet.Services
{
    public class WalletService : IWalletService
    {
        WalletDataContext wallet;
        int TransactionCount = 0;

        public WalletService() {
            wallet = new WalletDataContext();
        }

        public void Create(string name,decimal amount)
        {
            int UserCount = wallet.users.Count;
            User NewUser = new User(UserCount+1,name,amount);
            wallet.users.Add(NewUser);
            Console.WriteLine("Account created for {0} with user id {1}", name, UserCount + 1);
        }

        public void Transfer(int from, int to , decimal amount)
        {
            if(ValidateUsers(from, to) == false) return;
            User fromUser = wallet.users[from - 1];
            User toUser = wallet.users[to - 1];
            if (fromUser != null && toUser != null)
            {
                if (fromUser.account.balance >= amount)
                {
                    fromUser.account.balance -= amount;
                    toUser.account.balance += amount;
                    TransactionCount++;
                    Transaction transaction = new Transaction() { Id = TransactionCount, Amount = amount, FromAccount = from, ToAccount = to };
                    fromUser.account.transactions.Add(transaction);
                    toUser.account.transactions.Add(transaction);
                }
                else
                {
                    Console.WriteLine("Insufficient Balance");
                }
            }
            else
            {
                Console.WriteLine("User Not Found");
            }
        }

        private bool ValidateUsers(int from, int to)
        {
            if (from > wallet.users.Count)
            {
                Console.WriteLine("From user does not exist.Try again");
                return false;
            }
            else if (to > wallet.users.Count)
            {
                Console.WriteLine("To user does not exist.Try again");
                return false;
            }
            else if (from == to)
            {
                Console.WriteLine("From and to user are same");
                return false;
            }
            return true;
        }

        public void AccountStatement(int userid)
        {
            if (userid > wallet.users.Count)
            {
                Console.WriteLine("User does not exist.Try again");
                return;
            }
            User user = wallet.users[userid-1];
            if (user.account.transactions.Count == 0)
            {
                Console.WriteLine("No Activity");
            }
            foreach(Transaction t in user.account.transactions)
            {
                Console.WriteLine(t.ToString());
            }
        }

        public void Overview()
        {
            foreach(User user in wallet.users)
            {
                Console.WriteLine(user.ToString());
            }
        }






    }
}
