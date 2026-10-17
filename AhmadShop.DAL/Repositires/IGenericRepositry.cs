
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
            CancellationToken cancellationToken = default);

        Task<T?> FindAsync(
            int id,
            CancellationToken cancellationToken = default);
    }
}
