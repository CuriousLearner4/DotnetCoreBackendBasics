using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketClient
{
    internal class Program
    {
        public static class HelperClass
        {
            public static async Task<IPAddress> GetMyMachineIpAddress()
            {
                var hostName = Dns.GetHostName();
                IPHostEntry localhost = await Dns.GetHostEntryAsync(hostName);
                IPAddress myIpAddress = localhost.AddressList[1];
                return myIpAddress;
                
            }
        }
        static async Task SocketClient()
        {
            byte[] ipAddress = [10, 11, 0, 2];
            IPAddress serverIpAddress = new IPAddress(ipAddress);
            IPEndPoint ipEndpoint = new IPEndPoint(serverIpAddress, 11000);
            using Socket client = new(ipEndpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            await client.ConnectAsync(ipEndpoint);
            while (true)
            {
                Console.WriteLine("Enter you message:");
                var message = Console.ReadLine();
                message = message + "<|EOM|>";
                var messageBytes = Encoding.UTF8.GetBytes(message);
                _ = await client.SendAsync(messageBytes, SocketFlags.None);
                Console.WriteLine($"Socket client sent message: \"{message}\"");
                var buffer = new byte[1_024];
                var recieved = await client.ReceiveAsync(buffer, SocketFlags.None);
                var response = Encoding.UTF8.GetString(buffer, 0, recieved);
                if (response != null)
                {
                    Console.WriteLine($"Socket client recieved acknowledgment: \"{response}\"");
                    break;
                }
            }
            client.Shutdown(SocketShutdown.Both);
        }
        static async Task Main(string[] args)
        {
            while(true)
            await SocketClient();
            
        }
    }
}
