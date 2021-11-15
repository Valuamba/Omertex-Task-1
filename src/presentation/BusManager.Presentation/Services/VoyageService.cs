using BusManager.Application.Contracts;
using BusManager.Application.Contracts.Voyage;
using BusManager.Domain.Models;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusManager.Presentation.Services
{
    public interface IVoyageService
    {
        Task<VoyageInfoRequest[]> GetAll();
        Task<VoyageInfoRequest> GetVoyageById(int voyageId);
        Task<VoyageInfoRequest[]> SearchVoyage(string from = null, string to = null, DateTime? departureDateTime = null, string voyageName = null);
        Task<PagingResponse<VoyageInfoRequest>> GetVoyages(VoyageParameters voyageParameters);
    }

    public class VoyageService : IVoyageService
    {
        private readonly IHttpService _httpService;
        private readonly HttpClient _httpClient;
        private readonly IJSConsole _jsConsole;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public VoyageService(IHttpService httpService, HttpClient httpClient, IJSConsole jsConsole)
        {
            _httpService = httpService;
            _httpClient = httpClient;
            _jsConsole = jsConsole;
            _jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        }

        public async Task<VoyageInfoRequest> GetVoyageById(int voyageId)
        {
            return await _httpService.Get<VoyageInfoRequest>($"api/voyage/{voyageId}");
        }

        public async Task<VoyageInfoRequest[]> GetAll()
        {
            return await _httpService.Get<VoyageInfoRequest[]>("api/voyage/all");
        }

        public async Task<PagingResponse<VoyageInfoRequest>> GetVoyages(VoyageParameters voyageParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = voyageParameters.PageNumber.ToString(),
                ["from"] = voyageParameters.From,
                ["to"] = voyageParameters.To,
                ["voyageName"] = voyageParameters.VoyageName,
                ["departureDate"] = voyageParameters.DepartureDate?.ToString(),
            };

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("api/voyage/all", 
                queryStringParam.Where(x => x.Value != null).ToDictionary(k => k.Key, v => v.Value)));

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            
            return new PagingResponse<VoyageInfoRequest>
            {
                Items = JsonSerializer.Deserialize<List<VoyageInfoRequest>>(content, _jsonSerializerOptions),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _jsonSerializerOptions)
            };
        }

        public async Task<VoyageInfoRequest[]> SearchVoyage(string from = null, string to = null, DateTime? departureDateTime = null, string voyageName = null)
        {
            var url = $"api/voyage/search/{from}?";

            if(!string.IsNullOrEmpty(to))
            {
                url += $"to={to}&";
            }
            
            if(departureDateTime.HasValue)
            {
                url += $"departureDateTime={departureDateTime.Value.ToString("MM-dd-yyyy")}&";
            }

            if (!string.IsNullOrEmpty(voyageName))
            {
                url += $"voyageName={voyageName}&";
            }

            return await _httpService.Get<VoyageInfoRequest[]>(url);
        }
    }
}
