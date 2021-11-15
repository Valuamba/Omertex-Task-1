using Blazored.LocalStorage;
using BusManager.Presentation.AuthProviders;
using BusManager.Presentation.HttpRepository;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusManager.Presentation.Services;

namespace BusManager.Presentation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services
               .AddScoped<IAuthenticationService, AuthenticationService>()
               .AddScoped<IUserService, UserService>()
               .AddSingleton<IJSConsole, JSConsole>()
               .AddScoped<IOrderService, OrderService>()
               .AddScoped<IVoyageService, VoyageService>()
               .AddScoped<IHttpService, HttpService>()
               .AddScoped<ITicketService, TicketService>();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5010/") });

            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

            await builder.Build().RunAsync();
        }
    }
}
