﻿using Store_X.Domain.Contracts;
using Store_X.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store_X.Services.Specificatios
{
    internal class BaseSpecifications<Tkey, TEntity> : ISpecifications<Tkey, TEntity> where TEntity : BaseEntity<Tkey>
    {
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, bool>>? Criteria { get; set; }

        public BaseSpecifications(Expression<Func<TEntity, bool>> expression)
        {
            Criteria = expression;
        }
    }
}
