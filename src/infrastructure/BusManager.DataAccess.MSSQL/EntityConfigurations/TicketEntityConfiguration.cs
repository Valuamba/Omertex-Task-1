using BusManager.DataAccess.MSSQL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusManager.DataAccess.MSSQL.EntityConfigurations
{
    internal class TicketEntityConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(t => t.TicketId);

            builder
              .HasOne(t => t.Order)
              .WithOne(o => o.Ticket)
              .HasForeignKey<Ticket>(t => t.OrderId)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
