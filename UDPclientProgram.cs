﻿using System;
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

            //构建TCP 服务器
            Console.WriteLine("我是UDP客户机, 我的名字是{0}", Dns.GetHostName());

            //设置服务IP，设置TCP端口号
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 10159);
            //定义网络类型，数据连接类型和网络协议UDP
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            string welcome = "welcome,server! ";
            data = Encoding.Default.GetBytes(welcome);
            server.SendTo(data, data.Length, SocketFlags.None, ip);
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)sender;
            data = new byte[1024];
            //对于不存在的IP地址，加入此行代码后，可以在指定时间内解除阻塞模式限制
            int recv = server.ReceiveFrom(data, ref Remote);
            Console.WriteLine("收到来自{0}的信息: ", Remote.ToString());
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
            Console.WriteLine("正在退出...");
            server.Close();
        }

    }
}