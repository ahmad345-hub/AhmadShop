
using AhmadShop.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace AhmadShop.DAL.Repositires
{
    public class GenericRepository<T> : IGenericRepositry<T>
        where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(
            T entity,
            CancellationToken cancellationToken = default)
        {
            await _context.Set<T>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(
            T entity,
            CancellationToken cancellationToken = default)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveAsync(
            T entity,
            CancellationToken cancellationToken = default)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<T>> GetAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>()
                .ToListAsync(cancellationToken);
        }

        public async Task<T?> FindAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>()
                .FindAsync(new object[] { id }, cancellationToken);
        }
    }
}
