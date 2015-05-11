using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoZoom.Mongo.PersistencyEngine
{

    public static class MongoFactory
    {

        public MongoFactory() {
            // Connection string for using remote connections/Replica sets
            // mongodb://[username:password@]host1[:port1][,host2[:port2],...[,hostN[:portN]]][/[database][?options]]
            this.client = new MongoClient("mongodb://localhost");
            database = client.GetDatabase("ChronoZoom");
        }

        public MongoClient client { private get; private set; }

        public static IMongoDatabase database { get; private set; }


    }
}
