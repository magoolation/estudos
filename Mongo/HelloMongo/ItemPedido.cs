using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloMongo
{
    public class ItemPedido
    {
        public Produto Produto { get; set; }
        public double Quantidade { get; set; }

        public double Total()
        {
            return this.Produto.Preco*this.Quantidade;
        }
    }
}