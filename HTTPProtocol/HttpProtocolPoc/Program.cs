using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HttpProtocolPoc
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //var client = new CustomHttpClient();
            //client.Get(new Uri("https://www.geeksforgeeks.org/types-of-socket/"));
            await SocketServer();

        }


        static async Task SocketServer()
        {

            //Console.WriteLine(ipAddress);
            var ipAddress = HelperClass.GetMyMachineIpAddress().Result;

            IPEndPoint ipEndPoint = new(ipAddress, 11000);
            using Socket listner = new(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listner.Bind(ipEndPoint);
            listner.Listen(10);
            while (true)
            {
                var handler = await listner.AcceptAsync();
                var buffer = new byte[1_024];
                var received = handler.Receive(buffer, SocketFlags.None);
                
                Console.WriteLine(received);
                var response = Encoding.UTF8.GetString(buffer, 0, received);
                Console.WriteLine(
                       $"Socket server received message: \"{response ?? "No Message"}\"");
                Console.WriteLine("Enter you message");
                var message = Console.ReadLine();
                var Response = Encoding.UTF8.GetBytes(message);
                await handler.SendAsync(Response, SocketFlags.None);
                Console.WriteLine(
                    $"Socket server sent acknowledgment: \"{message}\"");
            }

        }
    }
}
