﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using eStore_Admin.Application.Interfaces.Persistence.Shared;
using eStore_Admin.Application.Utility;
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

        public async Task<IEnumerable<T>> GetAllPagedAsync(PagingParameters pagingParameters, bool trackChanges,
            CancellationToken cancellationToken)
        {
            var entities = DbSet
                .OrderBy(e => e.Id)
                .Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize)
                .Take(pagingParameters.PageSize);
            return trackChanges
                ? await entities
                    .ToListAsync(cancellationToken)
                : await entities
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> GetByConditionPagedAsync(Expression<Func<T, bool>> condition,
            PagingParameters pagingParameters, bool trackChanges, CancellationToken cancellationToken)
        {
            var entities = DbSet
                .Where(condition)
                .OrderBy(e => e.Id)
                .Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize)
                .Take(pagingParameters.PageSize);
            return trackChanges
                ? await entities
                    .ToListAsync(cancellationToken)
                : await entities
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
        }

        public async Task<T> GetByIdAsync(int id, bool trackChanges, CancellationToken cancellationToken)
        {
            return trackChanges
                ? await DbSet
                    .FirstOrDefaultAsync(c => c.Id == id, cancellationToken)
                : await DbSet
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
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