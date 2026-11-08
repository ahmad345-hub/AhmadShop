
using AhmadShop.DAL.DTO.Response;
using AhmadShop.DAL.Model;
using Microsoft.AspNetCore.Identity;

namespace AhmadShop.BLL.Settings
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            if (request.Password != request.ConfirmPassword)
            {
                throw new Exception("Password and Confirm Password do not match.");
            }

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingEmail != null)
            {
                throw new Exception("Email already exists.");
            }

            var existingUserName = await _userManager.FindByNameAsync(request.UserName);

            if (existingUserName != null)
            {
                throw new Exception("Username already exists.");
            }

            var user = new ApplicationUser
            {
                FullName = request.FullName,
                Email = request.Email,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ",
                    result.Errors.Select(e => e.Description)));
            }

            var roleResult = await _userManager.AddToRoleAsync(user, "User");

            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);

                throw new Exception(string.Join(", ",
                    roleResult.Errors.Select(e => e.Description)));
            }

            return new RegisterResponse
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber
            };
        }
    }
}
