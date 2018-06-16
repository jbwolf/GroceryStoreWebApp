using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MongoDB.Driver;

namespace GroceryStoreWebApp.App_Start
{
    public class MongoDBContext
    {
        public IMongoDatabase db;

        public MongoDBContext()
        {
            var client = new MongoClient(ConfigurationManager.AppSettings["MongoDBHost"]);
            db = client.GetDatabase(ConfigurationManager.AppSettings["MongoDBName"]);
        }
    }
}