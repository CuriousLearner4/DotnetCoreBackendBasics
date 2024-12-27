

namespace DigitalWallet.Models
{
    public class Account
    {
        public int Id { get; set; } 

        public decimal balance { get; set; }

        public List<Transaction> transactions {  get; set; }    
       
        public Account(int id, decimal balance)
        {
            this.Id = id;
            this.balance = balance; 
            transactions = new List<Transaction>();
        }
    }
}
