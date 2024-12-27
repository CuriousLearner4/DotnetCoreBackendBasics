using DigitalWallet.Services;

namespace DigitalWallet
{
    internal class Program
    {
        static void Main(string[] args)
        {
           MainService Service = new MainService(new WalletService());
           Service.serve();

        }
    }
}
