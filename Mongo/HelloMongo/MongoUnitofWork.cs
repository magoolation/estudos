using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;

namespace HelloMongo
{
    public class MongoUnitofWork
    {                
        private readonly MongoClient client = null;
        private readonly MongoServer server = null;
        private readonly MongoDatabase database = null;

        public MongoUnitofWork()
        {
            client = new MongoClient(@"mongodb://localhost");
            server = client.GetServer();
            database = server.GetDatabase("hellomongo");
        }

        public TEntity  Add<TEntity>(TEntity entity)
            where TEntity : IEntity
        {
            var collection = GetCollection<TEntity>();
            collection.Insert(entity);
            return entity;
        }        

        public void Modify<TEntity>(TEntity entity)
            where TEntity: IEntity
        {
            var collection = GetCollection<TEntity>();
            collection.Save(entity);
        }

        public void Remove<TEntity>(TEntity entity)
            where TEntity: IEntity
        {
            var query = Query<TEntity>.EQ(e => e.Id, entity.Id);
            var collection = GetCollection<TEntity>();
            collection.Remove(query);
        }

        public IQueryable<TEntity> CreateSet<TEntity>()
            where TEntity: IEntity
        {
            return GetCollection<TEntity>().AsQueryable();
        }

        private MongoCollection<TEntity> GetCollection<TEntity>()
            where TEntity : IEntity
        {
            var collectionName = typeof (TEntity).Name;
            return database.GetCollection<TEntity>(collectionName);
        }
    }
}