using BusManager.Application.Contracts;
using BusManager.Application.Contracts.Voyage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusManager.Presentation.Services
{
    public interface IUserService
    {
        Task<string> GetUserByEmail(string email);
        Task<UserInfo> GetUserInfo();
    }

    public class UserService : IUserService
    {
        private IHttpService _httpService;

        public UserService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<string> GetUserByEmail(string email)
        {
            return await _httpService.Get<string>($"api/account/user/{email}");
        }

        public async Task<UserInfo> GetUserInfo()
        {
            return await _httpService.Get<UserInfo>($"api/user");
        }
    }
}
