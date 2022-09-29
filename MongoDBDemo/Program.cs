using System;
using System.Collections.Generic;

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
    }
}
