using BusManager.DataAccess.MSSQL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusManager.DataAccess.MSSQL.EntityConfigurations
{
    internal class VoyageInfoEntityConfiguration : IEntityTypeConfiguration<VoyageInfo>
    {
        public void Configure(EntityTypeBuilder<VoyageInfo> builder)
        {
            builder.HasKey(v => v.Id);

            builder
                .HasOne(v => v.ArrivalBusStop)
                .WithMany(v => v.ArrivalBusStopVoyages)
                .HasForeignKey(v => v.ArrivalBusStopId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasOne(v => v.DepartureBusStop)
               .WithMany(v => v.DepartureBusStopVoyages)
               .HasForeignKey(v => v.DepartureBusStopId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
