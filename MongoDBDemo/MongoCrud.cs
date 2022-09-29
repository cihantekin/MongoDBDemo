using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDBDemo
{
    public class MongoCrud
    {
        private IMongoDatabase db;

        public MongoCrud(string database)
        {
            MongoClient client = new MongoClient();
            db = client.GetDatabase(database);
        }

        public void InsertRecord<T>(string table, T record)
        {
            IMongoCollection<T> collection = db.GetCollection<T>(table);
            collection.InsertOne(record);
        }

        public List<T> LoadRecords<T>(string table)
        {
            IMongoCollection<T> collection = db.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();
        }

        public T LoadRecordById<T>(string table, Guid id)
        {
            IMongoCollection<T> collection = db.GetCollection<T>(table);
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("Id", id);
            return collection.Find(filter).First();
        }

        public void UpsertRecord<T>(string table, Guid id, T record)
        {
            IMongoCollection<T> collection = db.GetCollection<T>(table);
            ReplaceOneResult result = collection.ReplaceOne(new BsonDocument("_id", id), record, new UpdateOptions {IsUpsert = true});
        }

        public void DeleteRecord<T>(string table, Guid id)
        {
            IMongoCollection<T> collection = db.GetCollection<T>(table);
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("Id", id);
            collection.DeleteOne(filter);
        }
    }
}