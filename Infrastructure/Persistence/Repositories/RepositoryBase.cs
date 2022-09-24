using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using eStore_Admin.Application.Interfaces.Persistence.Shared;
using eStore_Admin.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eStore_Admin.Infrastructure.Persistence.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : Entity
    {
        protected readonly ApplicationContext Context;
        protected readonly DbSet<T> DbSet;

        public RepositoryBase(ApplicationContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool trackChanges)
        { 
            return trackChanges 
                ? await DbSet
                    .ToListAsync() 
                : await DbSet
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllByConditionAsync(Expression<Func<T, bool>> condition, bool trackChanges)
        {
            return trackChanges 
                ? await DbSet
                    .Where(condition)
                    .ToListAsync() 
                : await DbSet
                    .Where(condition)
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id, bool trackChanges)
        {
            return trackChanges 
                ? await DbSet
                    .FirstOrDefaultAsync(c => c.Id == id) 
                : await DbSet
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == id);
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }
    }
}