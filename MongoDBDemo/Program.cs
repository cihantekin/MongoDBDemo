using DnsClient.Internal;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MongoDBDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoCrud db = new MongoCrud("AddressBook");
            //PersonModel person = new PersonModel
            //{
            //    Firstname = "Joe",
            //    Lastname = "Smith",
            //    PrimaryAddress = new AddressModel
            //    {
            //        StreetAddress = "101 Oak Street",
            //        City = "Scranton",
            //        State = "PA",
            //        ZipCode = "18512"
            //    }
            //};

            //db.InsertRecord("Users", person);

            //List<PersonModel> recs = db.LoadRecords<PersonModel>("Users");

            //foreach (PersonModel rec in recs)
            //{
            //    Console.WriteLine($"{rec.Id} : {rec.Firstname} {rec.Lastname}");

            //    if (rec.PrimaryAddress != null)
            //    {
            //        Console.WriteLine(rec.PrimaryAddress.City);
            //    }
            //    Console.WriteLine();
            //}

            List<NameModel> recs = db.LoadRecords<NameModel>("Users");

            foreach (var rec in recs)
            {
                Console.WriteLine($"{rec.Firstname} {rec.Lastname}");

            
                Console.WriteLine();
            }

            //PersonModel record = db.LoadRecordById<PersonModel>("Users", new Guid("a20e80e9-4e15-46f7-8924-7a141345e68b"));
            //record.DateOfBirth = new DateTime(1990, 10, 07, 0, 0, 0, DateTimeKind.Utc);
            //db.UpsertRecord("Users", record.Id, record);

            // db.DeleteRecord<PersonModel>("Users",record.Id);

            Console.ReadLine();
        }

        public void CreateExpireIndex(string? documentDuration, string? expireFormat)
        {
            IMongoDatabase db;
             MongoClient client = new MongoClient();
            db = client.GetDatabase("AddressBook");
            IMongoCollection<PersonModel> collection = db.GetCollection<PersonModel>("personModel");

            var isValidExpireFormat = TimeSpan.TryParseExact(documentDuration, expireFormat, CultureInfo.InvariantCulture, out TimeSpan expireAfter);

            if (isValidExpireFormat)
            {
                const string _expireAt = "ExpireAt";
                var expireIndex = new IndexKeysDefinitionBuilder<PersonModel>().Ascending(c => c.DateOfBirth);

                var index = collection.Indexes.List().ToList()
                    .Where(index => index["name"] == _expireAt)
                    .Select(a => new
                    {
                        ExpireAfterSeconds = a.GetElement("expireAfterSeconds").Value.ToString()
                    }).FirstOrDefault();

                if (index != null)
                {
                    var registeredExpireAfter = TimeSpan.FromSeconds(Convert.ToInt64(index.ExpireAfterSeconds));
                    if (registeredExpireAfter == expireAfter)
                    {
                        return;
                    }
                    collection.Indexes.DropOne(_expireAt);
                    
                }

                collection.Indexes.CreateOne(new CreateIndexModel<PersonModel>(expireIndex, new CreateIndexOptions<PersonModel>
                {
                    Name = _expireAt,
                    ExpireAfter = expireAfter,
                    //PartialFilterExpression = Builders<collection>.Filter.Eq(x => x.IsTemporaryDocument, true)
                }));
            }
        }
    }
}
