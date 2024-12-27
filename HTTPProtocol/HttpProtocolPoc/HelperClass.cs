using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpProtocolPoc
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
}
