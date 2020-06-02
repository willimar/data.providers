using data.provider.core.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace data.provider.core.mysql
{
    public class DataSet<TEntity> : IDataSet<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        protected DbSet<TEntity> _dbSet;

        public DataSet(DbContext context)
        {
            this._context = context;
            this._dbSet = context.Set<TEntity>();
        }

        public void Append(IEnumerable<TEntity> entity)
        {
            this._dbSet.AddRange(entity);
            this._context.SaveChanges();
        }

        public long Count(Expression<Func<TEntity, bool>> predicate)
        {
            return this._dbSet.Where(predicate).Count();
        }

        public long DeleteRecords(Expression<Func<TEntity, bool>> predicate)
        {
            var range = this.GetEntities(predicate);

            if (!range.Any())
            {
                return 0;
            }

            this._dbSet.RemoveRange(range);

            return range.Count();
        }

        public void Dispose()
        {
            this._dbSet = null;
        }

        public IEnumerable<TEntity> GetEntities(Expression<Func<TEntity, bool>> predicate, int limit = 0, int page = 0)
        {
            if (page > 0 && limit > 0)
            {
                return this._dbSet.Where(predicate).Skip((page - 1) * limit).Take(limit);
            }

            if (limit > 0)
            {
                return this._dbSet.Where(predicate).Take(limit);
            }
            else
            {
                return this._dbSet.Where(predicate);
            }
        }

        public IEnumerable<TEntity> GetEntities(Expression<Func<TEntity, bool>> predicate, List<Expression<Func<TEntity, object>>> sortFields, int limit = 0, int page = 0)
        {
            var find = this._dbSet.Where(predicate);

            foreach (var item in sortFields)
            {
                find = find.OrderBy(item);
            }

            if (page > 0 && limit > 0)
            {
                find = find.Skip((page - 1) * limit).Take(limit);
            }

            if (limit > 0)
            {
                find = find.Take(limit);
            }

            return find.ToList();
        }

        public long UpdateRecords(Expression<Func<TEntity, bool>> predicate, TEntity entity)
        {
            var count = this.Count(predicate);

            if (count == 0)
            {
                return 0;
            }

            if (count > 1)
            {
                throw new DataUpdateException($"Many records fount (Records found: {count}).");
            }

            this._dbSet.Update(entity);
            this._context.SaveChanges();

            return 1;
        }
    }
}
