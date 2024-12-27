using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DigitalWallet.Services.Interface;

namespace DigitalWallet.Services
{
    public class MainService
    {
        IWalletService WalletService;

        public MainService(IWalletService walletService) {
            WalletService = walletService;
        }
        public void serve()
        {

            Console.WriteLine("Welcome to digital wallet");
            while (true) {
                Console.WriteLine("Select services\n1.Create Wallet\n2.Transfer Amount\n3.Print statment\n4.Overview\n5.exit");
                string? input = Console.ReadLine();
                switch (input) {
                    case "1":
                        InputCreate();
                        break;
                    case "2":
                        InputTransfer();
                        break;
                    case "3":
                        InputAccountStatement();
                        break;
                    case "4":
                        WalletService.Overview();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Try again");
                        break;

                }

            }
        }

        private void InputAccountStatement()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter Account no");
                    int accountNo = int.Parse(Console.ReadLine());
                    WalletService.AccountStatement(accountNo);
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void InputTransfer()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter From account");
                    int from = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter To account");
                    int to = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter amount");
                    decimal amount = decimal.Parse(Console.ReadLine());
                    WalletService.Transfer(from, to, amount);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void InputCreate()
        {
            string? name;
            decimal amount;
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter name");
                    name = Console.ReadLine();
                    if (name == "") throw new Exception("Please enter name");
                    break;
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            while (true)
            {
                try
                {
                    Console.WriteLine("Enter amount to deposit");
                    amount = decimal.Parse(Console.ReadLine());
                    break;
                } catch (Exception ex)
                {
                    
                    Console.WriteLine(ex.ToString());
                }
            }
            WalletService.Create(name, amount);

        }
    }


}

