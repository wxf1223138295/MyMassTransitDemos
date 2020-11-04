using System;
using RabbitMQ.Client;

namespace Host.TestMq
{
    class Program
    {
        static void Main(string[] args)
        {
            //创建连接工厂
            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = "admin",//用户名
                Password = "admin",//密码
                HostName = "115.159.155.126",//rabbitmq ip
                Port = 30011,
                VirtualHost = "my_vhost"
            };
            
            //创建连接
            var connection = factory.CreateConnection();
//创建通道
            var channel = connection.CreateModel();
//声明一个队列
            channel.QueueDeclare("hello", false, false, false, null);

            Console.WriteLine("\nRabbitMQ连接成功，请输入消息，输入exit退出！");

            Console.ReadKey();
        }
    }
}