using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace data.provider.core
{
    public interface IDataSet<TEntity>: IDisposable where TEntity : class
    {
        void Append(IEnumerable<TEntity> entity);
        long DeleteRecords(Expression<Func<TEntity, bool>> predicate);
        long UpdateRecords(Expression<Func<TEntity, bool>> predicate, TEntity entity);
        IEnumerable<TEntity> GetEntities(Expression<Func<TEntity, bool>> predicate, int limit = 0, int page = 0);
        IEnumerable<TEntity> GetEntities(Expression<Func<TEntity, bool>> predicate, List<Expression<Func<TEntity, object>>> sortFields, int limit = 0, int page = 0);
        long Count(Expression<Func<TEntity, bool>> predicate);

    }
}
