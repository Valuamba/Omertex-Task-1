using BusManager.DataAccess.MSSQL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusManager.DataAccess.MSSQL.EntityConfigurations
{
    internal class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.OrderId);

            builder
             .HasOne(o => o.Voyage)
             .WithMany(v => v.Orders)
             .HasForeignKey(o => o.VoyageId)
             .OnDelete(DeleteBehavior.Cascade);

            builder
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
