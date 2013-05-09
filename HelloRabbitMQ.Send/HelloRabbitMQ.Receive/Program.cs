using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HelloRabbitMQ.Receive
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
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(Exchange_Name, "fanout");
                    var queueName = channel.QueueDeclare();
                    channel.QueueBind(queueName, Exchange_Name, "");

                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(queueName, true, consumer);

                    while (true)
                    {
                        var e = (BasicDeliverEventArgs) consumer.Queue.Dequeue();

                        Console.WriteLine(Encoding.UTF8.GetString(e.Body));
                    }
                }
            }
        }
    }
}
