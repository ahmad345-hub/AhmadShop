
using AhmadShop.DAL.DTO.Requests;

namespace AhmadShop.BLL
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<CategoryResponse?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<bool> CreateAsync(
            CategoryRequest request,
            CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(
            int id,
            CategoryRequest request,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(
            int id,
            CancellationToken cancellationToken = default);
    }
}
