using System;
using System.Linq.Expressions;
using eStore.Admin.Domain.Entities;

namespace eStore.Admin.Application.Interfaces.Filtering;

public interface IPredicateFactory<TEntity, in TFilterModel> where TEntity : Entity
{
    Expression<Func<TEntity, bool>> CreateExpression(TFilterModel filterModel);
}