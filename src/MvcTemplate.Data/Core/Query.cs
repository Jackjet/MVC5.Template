﻿using AutoMapper.QueryableExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MvcTemplate.Data.Core
{
    public class Query<TModel> : IQuery<TModel>
    {
        public Type ElementType => Set.ElementType;
        public Expression Expression => Set.Expression;
        public IQueryProvider Provider => Set.Provider;

        private IQueryable<TModel> Set { get; set; }

        public Query(IQueryable<TModel> set)
        {
            Set = set;
        }

        public IQuery<TResult> Select<TResult>(Expression<Func<TModel, TResult>> selector)
        {
            return new Query<TResult>(Set.Select(selector));
        }
        public IQuery<TModel> Where(Expression<Func<TModel, Boolean>> predicate)
        {
            Set = Set.Where(predicate);

            return this;
        }

        public IQueryable<TView> To<TView>()
        {
            return Set.ProjectTo<TView>();
        }

        public IEnumerator<TModel> GetEnumerator()
        {
            return Set.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
