using Store_X.Domain.Entities;
using System.Linq.Expressions;

namespace Store_X.Domain.Contracts
{
    public interface ISpecifications<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        public List<Expression<Func<TEntity, object>>> Includes { get; set; }
        public Expression<Func<TEntity, bool>>? Criteria { get; set; }
    }
}
