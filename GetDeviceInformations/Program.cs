using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GetDeviceInformations
{
    class Program
    {
        static void Main(string[] args)
        {
            var check= System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

            if (check)
            {
                Console.WriteLine("Methods-1: "+Dns.GetHostEntry(Dns.GetHostName()).AddressList.First(f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString());
                Console.WriteLine("Methods-2: " + GetLocalIPAddress());
                Console.WriteLine("Methods-3: " + localIPAddress());
                Console.WriteLine("----------");
                string str = "";

                System.Net.Dns.GetHostName();

                IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(str);

                IPAddress[] addr = ipEntry.AddressList;

                string IP = "Your WiFi Ip Address Is: " + addr[addr.Length - 1].ToString();
                Console.WriteLine(IP);
                Console.WriteLine("Got IT-"+GetLocalIPv4(NetworkInterfaceType.Wireless80211));




                IPAddress GetLocalIPv4(NetworkInterfaceType networkInterfaceType)
                {
                    var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces()
                        .Where(i => i.NetworkInterfaceType == networkInterfaceType && i.OperationalStatus == OperationalStatus.Up);

                    foreach (NetworkInterface networkInterface in networkInterfaces)
                    {
                        var adapterProperties = networkInterface.GetIPProperties();

                        if (adapterProperties.GatewayAddresses.FirstOrDefault() != null)
                        {
                            foreach (UnicastIPAddressInformation ip in networkInterface.GetIPProperties().UnicastAddresses)
                            {
                                if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                                {
                                    return ip.Address;
                                }
                            }
                        }
                    }

                    return null;
                }

            }
           
            Console.ReadKey();
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        public static string localIPAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                localIP = ip.ToString();

                string[] temp = localIP.Split('.');

                if (ip.AddressFamily == AddressFamily.InterNetwork && temp[0] == "192")
                {
                    break;
                }
                else
                {
                    localIP = null;
                }
            }

            return localIP;
        }
    }
}
