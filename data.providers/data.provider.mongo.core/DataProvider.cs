using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace data.provider.core.mongo
{
    public class DataProvider: IDataProvider
    {
        private IMongoDatabase _dbConnection;

        public DataProvider(MongoClient context, string dataBase)
        {
            this._dbConnection = context.GetDatabase(dataBase);
        }

        public void Dispose()
        {
            this._dbConnection = null;
        }

        public IDataSet<TEntity> GetDataSet<TEntity>() where TEntity : class
        {
            return new DataSet<TEntity>(this._dbConnection);
        }
    }
}
