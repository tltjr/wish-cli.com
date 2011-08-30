using System.Collections.Generic;
using MongoDB.Bson;

namespace Display.Data
{
    public interface IBasicPersistenceProvider<T>
    {
        T FindOneByKey(string key, string value);

        IEnumerable<T> FindAllByKey(string key, string value);

        IEnumerable<T> FindAll();

        void Store(T entity);

        void Update(T entity);

        void DeleteById(ObjectId id);
    }
}
