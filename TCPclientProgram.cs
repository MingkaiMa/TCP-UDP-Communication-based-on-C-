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
            string sendString = null;//要发送的字符串
            byte[] sendData = null;//要发送的字节数组
            TcpClient client = null;//TcpClient实例
            NetworkStream stream = null;//网络流

            IPAddress remoteIP = IPAddress.Parse("127.0.0.1");//远程主机IP
            int remotePort = 10159;//远程主机端口
            Console.WriteLine("我是TCP客户机, 我的名字是{0}", Dns.GetHostName());
            while (true)//死循环
            {
                sendString = Console.ReadLine();//获取要发送的字符串
                sendData = Encoding.Default.GetBytes(sendString);//获取要发送的字节数组
                client = new TcpClient();//实例化TcpClient
                try
                {
                    client.Connect(remoteIP, remotePort);//连接远程主机
                }
                catch
                {
                    Console.WriteLine("连接超时，服务器没有响应！");//连接失败
                    Console.ReadKey();
                    return;
                }
                stream = client.GetStream();//获取网络流
                stream.Write(sendData, 0, sendData.Length);//将数据写入网络流
                stream.Close();//关闭网络流
                client.Close();//关闭客户端
            }
        }
    }
}
