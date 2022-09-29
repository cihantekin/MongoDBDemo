using System;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBDemo
{
    public class PersonModel
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public AddressModel PrimaryAddress { get; set; }
        [BsonElement("dob")]
        public DateTime DateOfBirth { get; set; }
    }
}