using BusManager.DataAccess.MSSQL;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BusManager.Domain.Repositories;
using BusManager.DataAccess.MSSQL.Repositories;
using BusManager.Domain.Services;
using BusManager.Application.Services;
using BusManager.Application.Services.Interfaces;

namespace BusManager.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(policy =>
            {
                policy.AddPolicy("CorsPolicy", opt => opt
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithExposedHeaders("X-Pagination"));
            });

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<DataAccessMappingProfile>();
                cfg.AddProfile<DataAccessMappingProfile>();
            });

            services.AddDbContext<MssqlBusManagerDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection"),
                      x => x.MigrationsAssembly(typeof(MssqlBusManagerDbContext).Assembly.FullName)));

            services.AddScoped<IHasher, Hasher>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IUserRepository, DataAccess.MSSQL.Repositories.UserRepository>();
            services.AddScoped<IVoyagesRepository, DataAccess.MSSQL.Repositories.VoyagesRepository>();
            services.AddScoped<IBusStopRepository, DataAccess.MSSQL.Repositories.BusStopRepository>();
            services.AddScoped<ITicketRepository, DataAccess.MSSQL.Repositories.TicketRepository>();
            services.AddScoped<IOrderRepository, DataAccess.MSSQL.Repositories.OrderRepository>();
            services.AddScoped<IVoyagesRepository, DataAccess.MSSQL.Repositories.VoyagesRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IVoyageService, VoyageService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IUserService, UserService>();

            var jwtSettings = Configuration.GetSection("JwtSettings");

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    //ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    //LifetimeValidator = new LifetimeValidator()
                    ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                    ValidAudience = jwtSettings.GetSection("validAudience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value))
                };
            });

            services.AddControllers();
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MssqlBusManagerDbContext mssqlContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                SeedDataMssql.AddData(mssqlContext);
            }

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseBlazorFrameworkFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
