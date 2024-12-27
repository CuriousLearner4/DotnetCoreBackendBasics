using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpProtocolPoc
{
    internal class CustomHttpClient
    {
        Socket _socket;
        const int _port = 80;
        public CustomHttpClient()
        {
            _socket = new Socket(SocketType.Stream,ProtocolType.Tcp);
        }
        public void Get(Uri uri)
        {
            byte[] requestBytes = Encoding.ASCII.GetBytes($@"GET {uri.AbsoluteUri} HTTP/1.1 Host:{uri.Host} Connection: Close");
            _socket.Connect(uri.Host, _port);
            int byteSent = 0;
            while (byteSent < requestBytes.Length)
            {
                byteSent += _socket.Send(requestBytes, byteSent, requestBytes.Length - byteSent, SocketFlags.None);
            }
            byte[] responseBytes = new byte[256];
            char[] responseChars = new char[256];
            while (true)
            {
                int bytesRecieved = _socket.Receive(responseBytes);
                if (bytesRecieved == 0) break;
                int charCount = Encoding.ASCII.GetChars(responseBytes, 0, bytesRecieved,responseChars,0);
                Console.WriteLine(responseChars, 0, charCount);
            }
        }

    }
}
