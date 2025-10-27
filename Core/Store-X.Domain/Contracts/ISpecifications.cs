using Store_X.Domain.Entities;
using System.Linq.Expressions;

namespace Store_X.Domain.Contracts
{
    public interface ISpecifications<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        List<Expression<Func<TEntity, object>>> Includes { get; set; }
        Expression<Func<TEntity, bool>>? Criteria { get; set; }
        Expression<Func<TEntity, object>>? OrderBy { get; set; }
        Expression<Func<TEntity, object>>? OrderByDescending { get; set; }
    }
}
