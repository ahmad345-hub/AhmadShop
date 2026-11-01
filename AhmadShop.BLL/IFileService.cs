using Microsoft.AspNetCore.Http;

namespace AhmadShop.BLL
{
    public interface IFileService
    {
        Task<string?> UploadAsync(
            IFormFile file,
            CancellationToken cancellationToken = default);

        void Delete(string fileName);
    }
}