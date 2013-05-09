using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace HelloMongo
{
    public interface IEntity
    {
        ObjectId Id { get; set; }
    }
}
