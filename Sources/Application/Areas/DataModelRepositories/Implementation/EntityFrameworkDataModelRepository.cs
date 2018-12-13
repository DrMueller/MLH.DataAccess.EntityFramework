using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Mmu.Mlh.DataAccess.Areas.DataModeling.Models;
using Mmu.Mlh.DataAccess.EntityFramework.Areas.DataModelRepositories.Servants.Implementation;

namespace Mmu.Mlh.DataAccess.EntityFramework.Areas.DataModelRepositories.Implementation
{
    public class EntityFrameworkDataModelRepository<T, TId> : IEntityFrameworkDataModelRepository<T, TId>
        where T : AggregateRootDataModel<TId>
    {
        private readonly DbContext _dbContext;

        public EntityFrameworkDataModelRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task DeleteAsync(TId id)
        {
            var entity = await LoadSingleAsync(f => f.Id.Equals(id));
            if (entity == null)
            {
                return;
            }

            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public Task<IReadOnlyCollection<T>> LoadAsync(Expression<Func<T, bool>> predicate)
        {
            var query = _dbContext.Set<T>().Where(predicate);
            query = AppendIncludes(query);
            var result = query.ToList();

            return Task.FromResult<IReadOnlyCollection<T>>(result);
        }

        public async Task<T> LoadSingleAsync(Expression<Func<T, bool>> predicate)
        {
            var dataModels = await LoadAsync(predicate);
            return dataModels.SingleOrDefault();
        }

        public virtual async Task<T> SaveAsync(T dataModelBase)
        {
            EntityEntry<T> entityEntry;
            var dbSet = _dbContext.Set<T>();

            if (dataModelBase.Id.Equals(default(TId)))
            {
                entityEntry = await dbSet.AddAsync(dataModelBase);
            }
            else
            {
                entityEntry = dbSet.Update(dataModelBase);
                var entryBeforeUpdate = await LoadSingleAsync(f => f.Id.Equals(dataModelBase.Id));
                var entityEntryBeforeUPdate = _dbContext.Entry(entryBeforeUpdate);
                EntityEntryStateServant.AlignEntityEntryStatesRecursively(entityEntryBeforeUPdate, entityEntry, _dbContext);
            }

            await _dbContext.SaveChangesAsync();
            EntityEntryStateServant.MarkAsDetachedRecursively(entityEntry, _dbContext);
            return entityEntry.Entity;
        }

        protected virtual IQueryable<T> AppendIncludes(IQueryable<T> query)
        {
            return query;
        }
    }
}