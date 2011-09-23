using System;
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
            try
            {
                return _collection.FindOne(query);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IEnumerable<Post> FindAllByKey(string key, string value)
        {
            if (null == key || null == value) return null;
            var query = Query.EQ(key, value);
            try
            {
                return _collection.Find(query);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IEnumerable<Post> FindAll()
        {
            return _collection.FindAll();
        }

        public void Store(Post entity)
        {
            try
            {
                _collection.Insert(entity);
            }
            catch (Exception e)
            {
            }
        }

        public void Update(Post updated)
        {
            DeleteById(EditId.Id);
            Store(updated);
        }

        public void DeleteById(ObjectId id)
        {
            var query = Query.EQ("_id", id);
            try
            {
                _collection.Remove(query);
            }
            catch (Exception e)
            {
            }
        }


        public Post FindOneById(string objectId)
        {
            if (null == objectId) return null;
            var query = Query.EQ("_id", new BsonObjectId(objectId));
            try
            {
                return _collection.FindOne(query);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IEnumerable<string> FindTags()
        {
            var result = new List<string>();
            try
            {
                foreach (var post in _collection.FindAll())
                {
                    var tags = post.Tags;
                    foreach (var tag in tags)
                    {
                        if (!result.Contains(tag))
                        {
                            result.Add(tag);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return result;
        }
    }
}