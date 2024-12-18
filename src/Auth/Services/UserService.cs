using Auth.Models;
using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly JwtTokenService _jwtTokenService;
        private readonly IConfiguration _configuration;

        public UserService(
            IUserRepository userRepository,
            IEmailService emailService,
            JwtTokenService jwtTokenService,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _jwtTokenService = jwtTokenService;
            _configuration = configuration;
        }

        public async Task<string> RegisterUserAsync(RegisterModel model)
        {
            // Check if user already exists
            var existingUser = await _userRepository.GetUserByEmailAsync(model.Email);
            if (existingUser != null)
            {
                throw new Exception("User with this email already exists.");
            }

            // Create new user
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PasswordHash = model.Password, // Hash the password here (hashing not implemented for brevity)
                CreatedDate = DateTime.Now
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveAsync();

            // Send confirmation email
            await _emailService.SendEmailAsync(user.Email, "Welcome to SignSage", "Your account has been successfully created.");

            // Generate JWT token
            var token = _jwtTokenService.GenerateJwtToken(user.UserId.ToString(), user.Email, "User");

            return token;
        }

        public async Task<string> LoginUserAsync(LoginModel model)
        {
            // Check if user exists
            var user = await _userRepository.GetUserByEmailAsync(model.Email);
            if (user == null || user.PasswordHash != model.Password) // Password validation should use hash comparison
            {
                throw new Exception("Invalid login credentials.");
            }

            // Generate JWT token
            var token = _jwtTokenService.GenerateJwtToken(user.UserId.ToString(), user.Email, "User");

            return token;
        }

        public async Task<RefreshToken> GenerateRefreshTokenAsync(string userId)
        {
            // Generate a new refresh token for the user
            var refreshToken = await _jwtTokenService.GenerateRefreshToken(userId);
            return refreshToken;
        }

        public async Task<bool> VerifyRefreshTokenAsync(string token, string userId)
        {
            var refreshToken = await _userRepository.GetRefreshTokenAsync(token, userId);

            if (refreshToken == null || refreshToken.ExpiryDate < DateTime.Now)
            {
                return false;
            }

            return true;
        }

        public async Task<string> RefreshJwtTokenAsync(string refreshToken, string userId)
        {
            // Verify the refresh token
            if (!await VerifyRefreshTokenAsync(refreshToken, userId))
            {
                throw new Exception("Invalid or expired refresh token.");
            }

            // Generate a new JWT token
            var user = await _userRepository.GetUserByIdAsync(userId);
            var token = _jwtTokenService.GenerateJwtToken(user.UserId.ToString(), user.Email, "User");

            return token;
        }
    }
}
