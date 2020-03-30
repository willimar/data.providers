using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace data.provider.core.mysql
{
    public class DataProvider : IDataProvider
    {
        private DbContext _dbContext;

        public DataProvider(DbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void Dispose()
        {
            this._dbContext?.Dispose();
            this._dbContext = null;
        }

        public IDataSet<TEntity> GetDataSet<TEntity>() where TEntity : class
        {
            return new DataSet<TEntity>(this._dbContext);
        }
    }
}
