
namespace DigitalWallet.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Account account { get; set; }

        public User(int id,string name,decimal balance) { 
            Id = id;
            Name = name;
            account = new Account(id, balance);
        }

        public override string ToString() {
            return $"User id : {Id} user-name: {Name} balance :{account.balance}"; 
        }

    }
}
