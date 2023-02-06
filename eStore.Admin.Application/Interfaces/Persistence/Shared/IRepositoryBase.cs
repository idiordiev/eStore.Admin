using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using eStore.Admin.Application.Utility;
using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Application.Interfaces.Persistence.Shared;

public interface IRepositoryBase<TEntity> where TEntity : Entity
{
    Task<IEnumerable<TEntity>> GetAllPagedAsync(PagingParameters pagingParameters, bool trackChanges,
        CancellationToken cancellationToken);

    Task<IEnumerable<TEntity>> GetByConditionPagedAsync(Expression<Func<TEntity, bool>> condition,
        PagingParameters pagingParameters, bool trackChanges, CancellationToken cancellationToken);

    Task<TEntity> GetByIdAsync(int id, bool trackChanges, CancellationToken cancellationToken);
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}