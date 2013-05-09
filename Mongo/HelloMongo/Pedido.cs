using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace HelloMongo
{
    public class Pedido : IEntity
    {
        public ObjectId Id { get; set; }

        public DateTime DataPedido { get; set; }
        public Cliente Cliente { get; set; }
        public HashSet<ItemPedido> Itens { get; set; } 

        public Pedido()
        {
            Itens = new HashSet<ItemPedido>();
        }
    }
}