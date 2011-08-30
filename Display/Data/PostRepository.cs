using System.Collections.Generic;
using Display.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Display.Data
{
    public class PostRepository : IBasicPersistenceProvider<Post>
    {
        private readonly MongoCollection<Post> _collection;
        private readonly ConnectionHelper<Post> _connectionHelper = new ConnectionHelper<Post>();

        public PostRepository()
        {
            var database = MongoDatabase.Create(_connectionHelper.ConnectionString);
            _collection = database.GetCollection<Post>("Posts");
        }

        public Post FindOneByKey(string key, string value)
        {
            if (null == key || null == value) return null;
            var query = Query.EQ(key, value);
            return _collection.FindOne(query);
        }

        public IEnumerable<Post> FindAllByKey(string key, string value)
        {
            if (null == key || null == value) return null;
            var query = Query.EQ(key, value);
            return _collection.Find(query);
        }

        public IEnumerable<Post> FindAll()
        {
            return _collection.FindAll();
        }

        public void Store(Post entity)
        {
            _collection.Insert(entity);
        }

        public void Update(Post updated)
        {
            DeleteById(EditId.Id);
            Store(updated);
        }

        public void DeleteById(ObjectId id)
        {
            var query = Query.EQ("_id", id);
            _collection.Remove(query);
        }


        public Post FindOneById(string objectId)
        {
            if (null == objectId) return null;
            var query = Query.EQ("_id", new BsonObjectId(objectId));
            return _collection.FindOne(query);
        }
    }
}