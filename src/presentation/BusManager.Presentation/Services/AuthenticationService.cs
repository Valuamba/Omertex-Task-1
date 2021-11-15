using Blazored.LocalStorage;
using BusManager.Application.Contracts.Account;
using BusManager.Presentation.AuthProviders;
using BusManager.Presentation.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusManager.Presentation.HttpRepository
{
    public interface IAuthenticationService
    {
        Task<RegistrationResponseDto> RegisterUser(UserForRegistrationDto userForRegistration);
        Task<AuthResponseDto> Login(UserForAuthenticationDto userForAuthentication);
        Task Logout();
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly IJSConsole _jsConsole;

        public AuthenticationService(HttpClient client, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage, 
            IJSConsole jsConsole)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
            _jsConsole = jsConsole;
        }

        public async Task<RegistrationResponseDto> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var content = JsonSerializer.Serialize(userForRegistration);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var registrationResult = await _client.PostAsync("api/accounts/registration", bodyContent);
            var registrationContent = await registrationResult.Content.ReadAsStringAsync();

            if (!registrationResult.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<RegistrationResponseDto>(registrationContent, _options);
                return result;
            }

            return new RegistrationResponseDto { IsSuccessfulRegistration = true };
        }

        public async Task<AuthResponseDto> Login(UserForAuthenticationDto userForAuthentication)
        {
            var content = JsonSerializer.Serialize(userForAuthentication);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var authResult = await _client.PostAsync("api/accounts/login", bodyContent);
            var authContent = await authResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<AuthResponseDto>(authContent, _options);

            if (!authResult.IsSuccessStatusCode)
                return result;

            await _jsConsole.LogAsync($"LOGGG: {result.Token}");
            await _localStorage.SetItemAsync("authToken", result.Token);
            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(userForAuthentication.Email);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);

            return new AuthResponseDto { IsAuthSuccessful = true };
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            _client.DefaultRequestHeaders.Authorization = null;
        }
    }
}
