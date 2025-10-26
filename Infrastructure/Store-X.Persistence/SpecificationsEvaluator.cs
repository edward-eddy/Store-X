using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Store_X.Domain.Contracts;
using Store_X.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_X.Persistence
{
    public static class SpecificationsEvaluator
    {
        // _context.Products.Include(P => P.Brand).Include(P => P.Type).Where(P => P.Id == key as int?).FirstOrDefaultAsync() as TEntity;
        public static IQueryable<TEntity> GetQuery<TKey, TEntity>(IQueryable<TEntity> inputQuery, ISpecifications<TKey, TEntity> spec) where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery;


            if (spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria); // _context.Products.Where(P => P.Id == key as int?)
            }


            // _context.Products.Where(P => P.Id == key as int?).Include(P => P.Brand)
            // _context.Products.Where(P => P.Id == key as int?).Include(P => P.Brand).Include(P => P.Type)
            spec.Includes.Aggregate(query, (query, IncludeExpression) => query.Include(IncludeExpression));

            return query;
        }
    }
}
