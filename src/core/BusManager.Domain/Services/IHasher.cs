using BusManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusManager.Domain.Services
{
    public interface IHasher
    {
        string HashPassword(User user, string password);
    }
}
