using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RabbitMQ.Client;

namespace HelloRabbitMQ.Send
{
    class Program
    {
        private const string Exchange_Name = "HelloWorld";
        
        static void Main(string[] args)
        {

            using (var connection = new ConnectionFactory()
                {
                    HostName = "localhost"
                }.CreateConnection())
            {
                using (var channel  = connection.CreateModel())
                {
                    channel.ExchangeDeclare(Exchange_Name, "fanout");
                    for (int i = 0; i < 1000; i++)
                    {
                        byte[] body = Encoding.UTF8.GetBytes(string.Format("Mensagem {0}", i));
                        channel.BasicPublish(Exchange_Name, "", false, null, body);
                    }
                }
            }

        }
    }
}
