using BusManager.DataAccess.MSSQL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusManager.DataAccess.MSSQL
{
    public class MssqlBusManagerDbContext : DbContext
    {
        public MssqlBusManagerDbContext(DbContextOptions<MssqlBusManagerDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
    }
}
