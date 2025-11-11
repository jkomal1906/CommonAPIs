using CommonAPIs.DTOs;
using CommonAPIs.ImpRepository;
using CommonAPIs.Models;
using CommonAPIs.Repository;
using CommonAPIs.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CommonAPIs.ImpService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly CommonAPIsDbContext _commonAPIsDbContext; // Added missing field declaration  
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository userRepository, IConfiguration configuration, IEmailService emailService, CommonAPIsDbContext commonAPIsDbContext, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _emailService = emailService;
            _commonAPIsDbContext = commonAPIsDbContext; // Fixed assignment  
            _passwordHasher = passwordHasher;
        }

        public async Task<RegistrationResponseDto> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(registrationRequestDto.Email);
            if (existingUser != null)
            {
                return new RegistrationResponseDto
                {
                    Email = registrationRequestDto.Email,
                    Message = "User already exists"
                };
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registrationRequestDto.Password);

            var user = new User
            {
                FirstName = registrationRequestDto.FirstName,
                LastName = registrationRequestDto.LastName,
                UserName = registrationRequestDto.UserName,
                Email = registrationRequestDto.Email,
                PasswordHash = passwordHash
            };

            var createdUser = await _userRepository.AddUserAsync(user);

            return new RegistrationResponseDto
            {
                UserId = createdUser.Id,
                UserName = createdUser.UserName,
                Email = createdUser.Email,
                Message = "Registration successful"
            };
        }



        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string enteredPassword, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedHash);
        }


        public LoginResponseDto Login(LoginRequestDto loginDto)
        {
            var user = _userRepository.GetUserByEmail(loginDto.Email);

            if (user == null || string.IsNullOrWhiteSpace(user.PasswordHash))
                throw new Exception("Invalid email or password");

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
            if (!isPasswordValid)
                throw new Exception("Invalid email or password");

            string token = GenerateJwtToken(user);

            return new LoginResponseDto
            {
                Email = user.Email,
                UserName = user.UserName ?? "",
                Token = token
            };
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //Forgot Password
        public async Task<ForgotPasswordResponseDto> ForgotPasswordAsync(ForgotPasswordRequestDto request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                return new ForgotPasswordResponseDto
                {
                    Email = request.Email,
                    Message = "User with this email does not exist."
                };
            }

            // Create a token or reset link (for now, simulate with GUID)
            var resetToken = Guid.NewGuid().ToString(); // Normally store this in DB with expiry

            // Create reset link
            var resetLink = $"https://yourfrontend.com/reset-password?token={resetToken}&email={request.Email}";

            // Send Email (simulate for now)
            await _emailService.SendEmailAsync(request.Email, "Password Reset", $"Click here to reset your password: {resetLink}");

            return new ForgotPasswordResponseDto
            {
                Email = request.Email,
                Message = "Password reset link sent to your email."
            };
        }

        // Reset Password
        public async Task<bool> ResetPasswordAsync(ResetPasswordRequestDto request)
        {
            var user = await _commonAPIsDbContext.Set<User>().FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
                return false;

            user.PasswordHash = HashPassword(request.NewPassword);
            await _commonAPIsDbContext.SaveChangesAsync();
            return true;
        }



    }
}
