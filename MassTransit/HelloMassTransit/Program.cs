using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace HelloMassTransit
{
    
    public class Mensagem
    {
        public string Texto { get; set; }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Bus.Initialize(sbc =>
                {
                    sbc.UseRabbitMq();
                    sbc.ReceiveFrom(@"rabbitmq://localhost/HelloMassTransit");
                    sbc.Subscribe(subs =>
                        {
                            subs.Handler<Mensagem>(msg => Console.WriteLine(msg.Texto));
                        });
                });
            
while(true)
{
    var mensagem = Console.ReadLine();
    if (string.IsNullOrEmpty(mensagem.Trim()))
        break;

    Bus.Instance.Publish(
                    new Mensagem()
                        {
                            Texto = mensagem
                        }
                    );
            }
            Console.ReadLine();
        }
    }
}
