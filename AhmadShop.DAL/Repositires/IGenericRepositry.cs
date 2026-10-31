
using System.Linq.Expressions;

namespace AhmadShop.DAL.Repositires
{
    public interface IGenericRepositry<T> where T : class
    {
        Task CreateAsync(
            T entity,
            CancellationToken cancellationToken = default);

        Task UpdateAsync(
            T entity,
            CancellationToken cancellationToken = default);

        Task RemoveAsync(
            T entity,
            CancellationToken cancellationToken = default);

        Task<List<T>> GetAsync(
            Expression<Func<T, bool>>? filter = null,
            List<Expression<Func<T, object>>>? includes = null,
            bool tracked = false,
            CancellationToken cancellationToken = default);

        Task<T?> GetOneAsync(
            Expression<Func<T, bool>>? filter = null,
            List<Expression<Func<T, object>>>? includes = null,
            bool tracked = false,
            CancellationToken cancellationToken = default);

        Task<T?> FindAsync(
            int id,
            CancellationToken cancellationToken = default);
    }
}
