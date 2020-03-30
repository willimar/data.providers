using System;
using System.Collections.Generic;
using System.Text;

namespace data.provider.core
{
    public interface IDataProvider: IDisposable
    {
        IDataSet<TEntity> GetDataSet<TEntity>() where TEntity : class;
    }
}
