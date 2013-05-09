using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace HelloMongo
{
    public class Produto: IEntity
    {
        public ObjectId Id { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
    }
}
