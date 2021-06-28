using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Common.Dto;
using DAL.Entities;
using DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Crypt = BCrypt.Net.BCrypt;

namespace BLL.Services
{
    public class AuthService
    {
        private readonly UserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(UserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        /// <summary>
        /// Method to create user
        /// </summary>
        public async Task CreateUser(UserCredentialsDto userCredentials)
        {
            var (email, password) = userCredentials;

            await _userRepository.CreateUser(new User(email, Crypt.HashPassword(password)));
        }

        /// <summary>
        /// Method to authenticate user
        /// </summary>
        /// <returns>JWT token used for auth</returns>
        public async Task<string> LoginUser(UserCredentialsDto userCredentials)
        {
            var (email, password) = userCredentials;
            
            var existingUser = await _userRepository
                .FindUser(u => u.Email == email
                && Crypt.Verify(password, u.PasswordHash));

            if (existingUser != null)
            {
                return CreateToken(existingUser.Email);
            }

            throw new ArgumentException("Email or password is incorrect");
        }
        
        private string CreateToken(string email)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["JWTSecret"]);
            var tokenHandler = new JwtSecurityTokenHandler();
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Email, email),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}