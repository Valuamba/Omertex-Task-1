using BusManager.DataAccess.MSSQL.Entities;
using BusManager.DataAccess.MSSQL.EntityConfigurations;
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

        public DbSet<Order> Orders { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<VoyageInfo> Voyages { get; set; }
        public DbSet<BusStop> BusStops { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BusStopEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TicketEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new VoyageInfoEntityConfiguration());
        }
    }
}
