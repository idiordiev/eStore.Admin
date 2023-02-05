using System;
using System.Linq.Expressions;
using eStore_Admin.Domain.Entities;

namespace eStore_Admin.Application.Interfaces.Filtering;

public interface IPredicateFactory<TEntity, in TFilterModel> where TEntity : Entity
{
    Expression<Func<TEntity, bool>> CreateExpression(TFilterModel filterModel);
}