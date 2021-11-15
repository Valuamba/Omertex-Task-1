using BusManager.Application.Contracts.Account;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusManager.Presentation.Services
{
    public interface IHttpService
    {
        Task<T> Get<T>(string uri);
        Task<T> Post<T>(string uri, object value);
    }

    public class HttpService : IHttpService
    {
        private HttpClient _httpClient;
        private NavigationManager _navigationManager;
        private Blazored.LocalStorage.ILocalStorageService _localStorageService;
        private IConfiguration _configuration;
        private IJSConsole _jsConsole;

        public HttpService(
            HttpClient httpClient,
            NavigationManager navigationManager,
            Blazored.LocalStorage.ILocalStorageService localStorageService,
            IConfiguration configuration,
            IJSConsole jsConsole
        )
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
            _configuration = configuration;
            _jsConsole = jsConsole;
        }

        public async Task<T> Get<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return await SendRequest<T>(request);
        }

        public async Task<T> Post<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            return await SendRequest<T>(request);
        }

        public async Task<T> SendRequest_1<T>(HttpRequestMessage request)
        {
            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            await _jsConsole.LogAsync(content);

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            return JsonSerializer.Deserialize<T>(content);
        }

        private async Task<T> SendRequest<T>(HttpRequestMessage request)
        {
            var token = await _localStorageService.GetItemAsStringAsync("authToken");

            var isApiUrl = !request.RequestUri.IsAbsoluteUri;

            //if (token != null && isApiUrl)
            //{
            //    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //}

            using var response = await _httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("logout");
                return default;
            }

            await _jsConsole.LogAsync(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<T>();
        }
    }
}
