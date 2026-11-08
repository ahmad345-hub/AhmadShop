using AhmadShop.DAL.DTO.Response;

namespace AhmadShop.BLL.Settings
{
    public interface IAuthService
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
    }
}