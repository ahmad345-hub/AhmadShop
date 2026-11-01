
using AhmadShop.DAL.DTO.Requests;
using AhmadShop.DAL.DTO.Response;

namespace AhmadShop.BLL
{
    public interface IProductService
    {
        Task<ProductResponse> CreateAsync(
            ProductRequest request,
            CancellationToken cancellationToken = default);

        Task<List<ProductResponse>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<ProductResponse?> GetByIdAsync(int id);

        Task<ProductResponse?> UpdateAsync(
            int id,
            ProductRequest request,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(int id);
    }
}
