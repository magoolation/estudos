using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMongo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //CreateClients();
            //CreateProducts();

            CreateOrders();

            Console.ReadLine();
        }

        private static void CreateClients()
        {
            var unitofWork = new MongoUnitofWork();

            unitofWork.Add(new Cliente()
                {
                    PrimeiroNome = "Alexandre",
                    UltimoNome = "Costa",
                    Email = "magoolation@me.com"
                });

            unitofWork.Add(new Cliente()
                {
                    PrimeiroNome = "Gabriel",
                    UltimoNome = "Vieira",
                    Email = "gabriel@l2you.com.br"
                });

            var clientes = unitofWork.CreateSet<Cliente>().ToList();

            foreach (var c in clientes)
            {
                Console.WriteLine(string.Format("{0}, {1} - {2}", c.UltimoNome, c.PrimeiroNome, c.Email));
            }
        }

        public static void CreateProducts()
        {
            var unitofWork = new MongoUnitofWork();

            unitofWork.Add(new Produto()
                {
                    Nome = "Coca-cola",
                    Preco = 4.0
                });

            unitofWork.Add(new Produto()
                {
                    Nome = "Pringles",
                    Preco = 8.5
                });

            var produtos = unitofWork.CreateSet<Produto>().ToList();

            foreach (var p in produtos)
            {
                Console.WriteLine(string.Format("{0} - {1}", p.Nome, p.Preco));
            }
        }

        private static void CreateOrders()
        {
            var unitofWork = new MongoUnitofWork();

            var clientes = unitofWork.CreateSet<Cliente>();
            var produtos = unitofWork.CreateSet<Produto>();

            unitofWork.Add(new Pedido()
                {
                    DataPedido = DateTime.Today,
                    Cliente =clientes.SingleOrDefault(c => c.UltimoNome == "Costa"),
                    Itens = new HashSet<ItemPedido>()
                        {
                            new ItemPedido()
                                {
                                    Produto = produtos.SingleOrDefault(p => p.Nome == "Coca-cola"),
                                    Quantidade = 10
                                },
                            new ItemPedido()
                                {                                    
                                    Produto = produtos.SingleOrDefault(p => p.Nome == "Pringles"),
                                    Quantidade = 15
                                }
                        }
                });

            var pedidos = unitofWork.CreateSet<Pedido>().ToList();

            foreach (var p in pedidos)
            {
                Console.WriteLine("Pedido para:");
                Console.WriteLine(string.Format("{0}, {1} em {2}", p.Cliente.UltimoNome, p.Cliente.PrimeiroNome, p.Cliente.Email
                    ));
                Console.WriteLine("Itens");
                foreach (var i in p.Itens)
                {
                    Console.WriteLine(string.Format("{0}\t{1} unidades a {2} totalizando {3}",
                                                    i.Produto.Nome, i.Quantidade, i.Produto.Preco, i.Total()));
                }
                Console.WriteLine();

                Console.WriteLine(p.Itens.Sum(i => i.Total()));
                
            }
        }
    }
}