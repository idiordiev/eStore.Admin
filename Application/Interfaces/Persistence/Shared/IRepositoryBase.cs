using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using eStore_Admin.Domain.Entities;

namespace eStore_Admin.Application.Interfaces.Persistence.Shared
{
    public interface IRepositoryBase<TEntity> where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges);
        Task<IEnumerable<TEntity>> GetAllByConditionAsync(Expression<Func<TEntity, bool>> condition, bool trackChanges);
        Task<TEntity> GetByIdAsync(int id, bool trackChanges);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}