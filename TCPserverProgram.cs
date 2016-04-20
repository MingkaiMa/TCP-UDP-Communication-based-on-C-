using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCPserver
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = null;
            NetworkStream stream = null;
            byte[] buffer = null;
            string receiveString = null;
            IPAddress localIP = IPAddress.Parse("127.0.0.1");
            int localPort = 10159;
            TcpListener listener = new TcpListener(localIP, localPort);//Using local IP and port to initiate Listener 
            Console.WriteLine("I am TCP server,my name is{0}", Dns.GetHostName());
            listener.Start();//start to listen
            while (true)
            {
                client = listener.AcceptTcpClient();//accept a Client
                buffer = new byte[client.ReceiveBufferSize];
                stream = client.GetStream();//get network stream
                stream.Read(buffer, 0, buffer.Length);//get the data from network stream
                stream.Close();//close the network stream
                client.Close();//close Client
                receiveString = Encoding.Default.GetString(buffer).Trim('\0');//convert to string
                Console.WriteLine(receiveString);
            }


        }
    }
}
