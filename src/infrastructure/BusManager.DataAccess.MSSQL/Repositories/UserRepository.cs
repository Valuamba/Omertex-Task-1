using AutoMapper;
using BusManager.DataAccess.MSSQL.Entities;
using BusManager.Domain.Models;
using BusManager.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusManager.DataAccess.MSSQL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MssqlBusManagerDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(MssqlBusManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var trackedUser = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);

            return _mapper.Map<UserEntity, User>(trackedUser);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var entityUser = _mapper.Map<User, UserEntity>(user);

            var trackedUser = _context.Add(entityUser);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserEntity, User>(trackedUser.Entity);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var trackedUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            return _mapper.Map<UserEntity, User>(trackedUser);
        }
    }
}
