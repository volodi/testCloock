using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cloocks.Server.Entities
{
    public interface IEntity
    {
        string Id { get; set; }
    }

    public class MongoEntity : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }

    public class Clock: MongoEntity
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Data { get; set; }
        public string Url { get; set; }
    }
}
