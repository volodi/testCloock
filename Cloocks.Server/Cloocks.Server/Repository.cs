using Cloocks.Server.Entities;
using Cloocks.Server.Extensions;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cloocks.Server
{
public class MongoRepository<T>
       where T : class, IEntity
    {
        private IMongoCollection<T> collection
        {
            get { return MongoFactory.GetCollection<T>(); }
        }

        #region Exists

        public async Task<bool> ExistsAsync(string id)
        {
            var entity = await collection.Find(x => x.Id == id)
                .Project(new ProjectionDefinitionBuilder<T>().Include("_id"))
                .SingleOrDefaultAsync();

            if (entity == null)
                return false;

            return !entity.IsBsonNull;
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            var entity = await collection.Find(predicate)
                 .Project(new ProjectionDefinitionBuilder<T>().Include("_id"))
                 .SingleOrDefaultAsync();

            if (entity == null)
                return false;

            return !entity.IsBsonNull;
        }

        public async Task<bool> ExistsManyAsync(IEnumerable<string> ids)
        {
            var objectIds = ids.Select(x => new ObjectId(x));

            var filter = Builders<T>.Filter.In("_id", objectIds);

            var entity = await collection.Find(filter)
                 .Project(new ProjectionDefinitionBuilder<T>().Include("_id"))
                 .ToListAsync();

            return entity.Count == ids.Count();
        }

        #endregion

        #region Find

        public async Task<T> FindAsync(string id)
        {
            var tempResult = await collection.FindAsync(x => x.Id == id);
            return tempResult.ToEnumerable().FirstOrDefault();
        }

        public async Task<IEnumerable<T>> FindManyAsync(Expression<Func<T, bool>> predicate = null,
                    Expression<Func<T, object>> order = null,
                    bool byDescending = false, int limit = 50, int skip = 0)
        {
            order = order ?? (x => x.Id);
            var sortDefinition = byDescending ? Builders<T>.Sort.Descending(order) : Builders<T>.Sort.Ascending(order);

            return await collection.Find(predicate).Sort(sortDefinition).Limit(limit).Skip(skip).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetSkip(int skip)
        {
            var entities = await collection.Find(new BsonDocument()).Skip(skip).Limit(2).ToListAsync();
            return entities;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var entities = await collection.Find(new BsonDocument()).ToListAsync();
            return entities;
        }

        #endregion

        #region Insert

        public async Task<bool> InsertOneAsync(T entity)
        {
            try
            {
                await collection.InsertOneAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region Update

        // Builders<Cloocks>.Update.Set(f => f.Name, "uhnfnfu").Set(f => f.Description, "")
        public async Task<bool> UpdateOneAsync(string id, UpdateDefinition<T> entity)
        {
            try
            {
                var updateRes = await collection.UpdateOneAsync(new BsonDocument("_id", new ObjectId(id)), entity);

                if (updateRes.ModifiedCount < 1)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion
    }
}
