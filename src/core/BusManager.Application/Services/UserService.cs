using BusManager.Application.Contracts.Account;
using BusManager.Domain.Models;
using BusManager.Domain.Repositories;
using BusManager.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusManager.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHasher _hasher;
        private readonly IConfigurationSection _jwtSettings;

        public UserService(IUserRepository userRepository, IHasher hasher, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _hasher = hasher;
            _jwtSettings = configuration.GetSection("JwtSettings");
        }

        public async Task<bool> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var duplicate = await _userRepository.GetUserByEmailAsync(userForRegistration.Email);

            if (userForRegistration == null || duplicate != null)
                return false;

            var user = new User { Email = userForRegistration.Email, };

            user.Password = _hasher.HashPassword(user, userForRegistration.Password);

            var newUser = await _userRepository.CreateUserAsync(user);

            return newUser != null;
        }

        public async Task<AuthResponseDto> Login(UserForAuthenticationDto userForAuthentication)
        {
            var user = await _userRepository.GetUserByEmailAsync(userForAuthentication.Email);

            var hash = _hasher.HashPassword(user, userForAuthentication.Password);

            if (user == null || hash != user.Password)
                throw new Exception("Invalid Authentication");

            var signingCredentials = GetSigningCredentials();
            var claims = GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return new AuthResponseDto { IsAuthSuccessful = true, Token = token };
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private List<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email)
            };

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtSettings.GetSection("validIssuer").Value,
                audience: _jwtSettings.GetSection("validAudience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.GetSection("expiryInMinutes").Value)),
                signingCredentials: signingCredentials);

            return tokenOptions;
        }
    }
}
