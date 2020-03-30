using data.provider.core.mongo;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace data.provider.core.test.Factories
{
    internal class MongoDataProviderFactory
    {
        public static IDataProvider Factory()
        {
            var connectionString = $"mongodb://localhost:27017/?readPreference=primary&appname=data.provider.core.test&ssl=false";
            var context = new MongoClient(connectionString);

            return new DataProvider(context, "appTest");
        }
    }
}
