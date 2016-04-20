using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Net;

namespace TCPclient
{
    class Program
    {
        static void Main(string[] args)
        {
            string sendString = null;//String that is going to be sent
            byte[] sendData = null;//Data that is going to be sent
            TcpClient client = null;//TcpClient instance
            NetworkStream stream = null;//network stream

            IPAddress remoteIP = IPAddress.Parse("127.0.0.1");//remote IP
            int remotePort = 10159;//remote port
            Console.WriteLine("I am TCP client, my name is{0}", Dns.GetHostName());
            while (true)//Endless loop
            {
                sendString = Console.ReadLine();//get the string
                sendData = Encoding.Default.GetBytes(sendString);//get the data
                client = new TcpClient();//create new instance of TcpClient 
                try
                {
                    client.Connect(remoteIP, remotePort);//connect remote 
                }
                catch
                {
                    Console.WriteLine("Connection timed out , the server does not respond!");//connection fail
                    Console.ReadKey();
                    return;
                }
                stream = client.GetStream();//get network stream
                stream.Write(sendData, 0, sendData.Length);//write the data into network stream
                stream.Close();//close net network stream
                client.Close();//close the applicaiton
            }
        }
    }
}
