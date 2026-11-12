using AhmadShop.BLL.Settings;
using AhmadShop.DAL.DTO.Requests;
using AhmadShop.DAL.DTO.Response;

namespace AhmadShop.BLL
{
    public interface IAuthService
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);

        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<bool> ConfirmEmailAsync(string userId, string token);
    }
}