
using AhmadShop.DAL.Model;

namespace AhmadShop.BLL
{
    public interface ITokenService
    {
        Task<string> GenerateToken(ApplicationUser user);
    }
}
