using ChronoZoom.Mongo.Models;
using ChronoZoom.Mongo.PersistencyEngine;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoZoom.Mongo.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoFactory mf = new MongoFactory();

            User user = UserFactory.FindByIdAsync(ObjectId.Parse("55437512f67ce7a253751fa0")).Result;
            List<Collection> collections = CollectionFactory.FindByUserIdAsync(ObjectId.Parse("55437512f67ce7a253751fa0")).Result;
            user.Collections = collections;

            Console.WriteLine(user.Name);
            Console.WriteLine(user.Email);
            Console.WriteLine(user.Collections);

            User newUser = new User("Max de Bruijn", "max.de.bruijn@gmail.com");

            Boolean created = UserFactory.InsertAsync(newUser).Result;

            Console.Read();
        }
    }
}
