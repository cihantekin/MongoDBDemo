using System;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBDemo
{
    [BsonIgnoreExtraElements]
    public class NameModel
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}