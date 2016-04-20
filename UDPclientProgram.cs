using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace UDPClient
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] data = new byte[1024];
            string input, stringData;

            //Create TCP server
            Console.WriteLine("I am UDP client,my name is{0}", Dns.GetHostName());

            //Setup service IP and TCP port
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 10159);
            //Define the type of network,data connection and network protocol UDP
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            string welcome = "welcome,server! ";
            data = Encoding.Default.GetBytes(welcome);
            server.SendTo(data, data.Length, SocketFlags.None, ip);
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)sender;
            data = new byte[1024];
            //For non-existent IP address, after adding this line of code, unblock mode within a specified time limitation
            int recv = server.ReceiveFrom(data, ref Remote);
            Console.WriteLine("receive message from{0}: ", Remote.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
            while (true)
            {
                input = Console.ReadLine();
                if (input == "exit")
                    break;
                server.SendTo(Encoding.ASCII.GetBytes(input), Remote);
                data = new byte[1024];
                recv = server.ReceiveFrom(data, ref Remote);
                stringData = Encoding.ASCII.GetString(data, 0, recv);
                Console.WriteLine(stringData);
            }
            Console.WriteLine("exit...");
            server.Close();
        }

    }
}
