using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace UDP
{
    class Program
    {
        static void Main(string[] args)
        {
            int recv;
            byte[] data = new byte[1024];

            //Get IP,setup TCP port        
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, 10159);
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //Bind network address
            newsock.Bind(ip);
            Console.WriteLine("I am UDP server,my name is{0}", Dns.GetHostName());
            //Wait for client connection
            Console.WriteLine("Waiting for client connection...");
            //get client IP
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)(sender);
            recv = newsock.ReceiveFrom(data, ref Remote);
            Console.WriteLine("receive message from{0}: ", Remote.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
            //Client connection success,send message
            string welcome = "welcome,client! ";
            
            data = Encoding.Default.GetBytes(welcome);
            //Send message
            newsock.SendTo(data, data.Length, SocketFlags.None, Remote);
            while (true)
            {
                data = new byte[1024];
                //Send received message
                recv = newsock.ReceiveFrom(data, ref Remote);
                Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
                newsock.SendTo(data, recv, SocketFlags.None, Remote);
            }
        }

    }
}
