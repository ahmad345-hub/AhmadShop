
using AhmadShop.DAL.DTO.Requests;
using AhmadShop.DAL.Model;
using AhmadShop.DAL.Repositires;
using Mapster;

namespace AhmadShop.BLL
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepositry<Category> _repo;

        public CategoryService(IGenericRepositry<Category> repo)
        {
            _repo = repo;
        }

        public async Task<bool> CreateAsync(
            CategoryRequest request,
            CancellationToken cancellationToken = default)
        {
            var categories = await _repo.GetAsync(cancellationToken);

            var exists = categories.Any(c =>
                c.Name.Equals(
                    request.Name,
                    StringComparison.OrdinalIgnoreCase));

            if (exists)
                return false;

            var category = request.Adapt<Category>();

            await _repo.CreateAsync(category, cancellationToken);

            return true;
        }

        public async Task<List<CategoryResponse>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            var categories = await _repo.GetAsync(cancellationToken);

            return categories.Adapt<List<CategoryResponse>>();
        }

        public async Task<CategoryResponse?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var category = await _repo.FindAsync(id, cancellationToken);

            return category?.Adapt<CategoryResponse>();
        }

        public async Task<bool> UpdateAsync(
            int id,
            CategoryRequest request,
            CancellationToken cancellationToken = default)
        {
            var category = await _repo.FindAsync(id, cancellationToken);

            if (category == null)
                return false;

            var categories = await _repo.GetAsync(cancellationToken);

            var exists = categories.Any(c =>
                c.Id != id &&
                c.Name.Equals(
                    request.Name,
                    StringComparison.OrdinalIgnoreCase));

            if (exists)
                return false;

            request.Adapt(category);

            await _repo.UpdateAsync(category, cancellationToken);

            return true;
        }

        public async Task<bool> DeleteAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var category = await _repo.FindAsync(id, cancellationToken);

            if (category == null)
                return false;

            await _repo.RemoveAsync(category, cancellationToken);

            return true;
        }
    }
}
