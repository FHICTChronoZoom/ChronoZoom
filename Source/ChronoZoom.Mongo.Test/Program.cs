﻿using ChronoZoom.Mongo.Models;
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
            UserFactory uf = new UserFactory();

            User user = uf.findById(ObjectId.Parse("554374f4f67ce7a253751f9f")).Result;

            Console.WriteLine(user.Name);
        }
    }
}
