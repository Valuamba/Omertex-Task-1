using BusManager.DataAccess.MSSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BusManager.API
{
    public class MssqlBusManagerContextFactory : IDesignTimeDbContextFactory<MssqlBusManagerDbContext>
    {
        public MssqlBusManagerDbContext CreateDbContext(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();

            var optionsBuilder = new DbContextOptionsBuilder<MssqlBusManagerDbContext>();

            optionsBuilder.UseSqlServer(config.GetConnectionString("SqlServerConnection"),
                x => x.MigrationsAssembly(typeof(MssqlBusManagerDbContext).Assembly.FullName));

            return new MssqlBusManagerDbContext(optionsBuilder.Options);
        }
    }
}
