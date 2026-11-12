using AhmadShop.BLL.Settings;
using AhmadShop.DAL.DTO.Response;
using AhmadShop.DAL.DTO.Requests;
using AhmadShop.DAL.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace AhmadShop.BLL
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService,
            IEmailService emailService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailService = emailService;
            _configuration = configuration;
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

            // Generate Email Confirmation Token
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            // Create Confirmation Link
            var appUrl = _configuration["AppUrl"];

            var confirmationLink =
                $"{appUrl}/api/Auth/ConfirmEmail?userId={user.Id}&token={WebUtility.UrlEncode(token)}";

            // Send Confirmation Email
            var emailBody = $@"
                <h2>Welcome to AhmadShop!</h2>
                <p>Please confirm your email address by clicking the link below:</p>
                <a href='{confirmationLink}'>Confirm Email</a>
            ";

            await _emailService.SendEmailAsync(
                user.Email!,
                "Confirm Your Email - AhmadShop",
                emailBody);

            return new RegisterResponse
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber
            };
        }

        public async Task<bool> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false;
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            return result.Succeeded;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new Exception("Invalid email or password.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                request.Password,
                lockoutOnFailure: true);

            if (result.IsLockedOut)
            {
                throw new Exception("Your account is locked. Please try again later.");
            }

            if (result.IsNotAllowed)
            {
                throw new Exception("Email is not confirmed.");
            }

            if (!result.Succeeded)
            {
                throw new Exception("Invalid email or password.");
            }

            var token = await _tokenService.GenerateToken(user);

            return new LoginResponse
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email!,
                UserName = user.UserName!,
                Token = token
            };
        }
    }
}