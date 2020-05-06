using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace data.provider.core.mongo
{
    public class DataSet<TEntity>: IDataSet<TEntity> where TEntity : class
    {
        private IMongoCollection<TEntity> _mongoCollection;

        public DataSet(IMongoDatabase database)
        {
            this._mongoCollection = database.GetCollection<TEntity>(typeof(TEntity).ToString()); ;
        }

        public void Append(IEnumerable<TEntity> entity)
        {
            this._mongoCollection.InsertMany(entity);
        }

        public long Count(Expression<Func<TEntity, bool>> predicate)
        {
            if (this._mongoCollection.Find(predicate).Any())
            {
                return this._mongoCollection.Find(predicate).CountDocuments();
            }

            return 0;
        }

        public long DeleteRecords(Expression<Func<TEntity, bool>> predicate)
        {
            var result = this._mongoCollection.DeleteMany(predicate);

            return result.DeletedCount;
        }

        public void Dispose()
        {
            this._mongoCollection = null;
        }

        public IEnumerable<TEntity> GetEntities(Expression<Func<TEntity, bool>> predicate, int limit = 0, int page = 0)
        {
            if (page > 0 && limit > 0)
            {
                return this._mongoCollection.Find(predicate).Skip((page - 1) * limit).Limit(limit).ToList();
            }

            if (limit > 0)
            {
                return this._mongoCollection.Find(predicate).Limit(limit).ToList();
            }
            
            return this._mongoCollection.Find(predicate).ToList();
        }

        public long UpdateRecords(Expression<Func<TEntity, bool>> predicate, TEntity entity)
        {
            var result = this._mongoCollection.ReplaceOne(predicate, entity);

            return result.MatchedCount;
        }
    }
}
