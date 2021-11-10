using BusManager.Domain.Models;
using BusManager.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusManager.Application.Services
{
    public class Hasher : IHasher
    {
        public string HashPassword(User user, string password)
        {
            using var alg = SHA256.Create();
            var hashBytes = alg.ComputeHash(Encoding.UTF8.GetBytes(password + user.Email));

            return Convert.ToHexString(hashBytes);
        }
    }
}
