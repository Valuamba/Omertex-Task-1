// <auto-generated />
using System;
using BusManager.DataAccess.MSSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BusManager.DataAccess.MSSQL.Migrations
{
    [DbContext(typeof(MssqlBusManagerDbContext))]
    partial class MssqlBusManagerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BusManager.DataAccess.MSSQL.Entities.BusStop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BusStops");
                });

            modelBuilder.Entity("BusManager.DataAccess.MSSQL.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("VoyageId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("UserId");

                    b.HasIndex("VoyageId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("BusManager.DataAccess.MSSQL.Entities.Ticket", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("PassengerDocumentNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassengerFirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassengerLastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PassengerSeatNumber")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("TicketId");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("BusManager.DataAccess.MSSQL.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BusManager.DataAccess.MSSQL.Entities.VoyageInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArrivalBusStopId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ArrivalDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("DepartureBusStopId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DepartureDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("NumberOfSeats")
                        .HasColumnType("int");

                    b.Property<decimal>("OneTicketCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TravelTimeMinutes")
                        .HasColumnType("int");

                    b.Property<string>("VoyageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VoyageNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ArrivalBusStopId");

                    b.HasIndex("DepartureBusStopId");

                    b.ToTable("Voyages");
                });

            modelBuilder.Entity("BusManager.DataAccess.MSSQL.Entities.Order", b =>
                {
                    b.HasOne("BusManager.DataAccess.MSSQL.Entities.UserEntity", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusManager.DataAccess.MSSQL.Entities.VoyageInfo", "Voyage")
                        .WithMany("Orders")
                        .HasForeignKey("VoyageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Voyage");
                });

            modelBuilder.Entity("BusManager.DataAccess.MSSQL.Entities.Ticket", b =>
                {
                    b.HasOne("BusManager.DataAccess.MSSQL.Entities.Order", "Order")
                        .WithOne("Ticket")
                        .HasForeignKey("BusManager.DataAccess.MSSQL.Entities.Ticket", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("BusManager.DataAccess.MSSQL.Entities.VoyageInfo", b =>
                {
                    b.HasOne("BusManager.DataAccess.MSSQL.Entities.BusStop", "ArrivalBusStop")
                        .WithMany("ArrivalBusStopVoyages")
                        .HasForeignKey("ArrivalBusStopId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BusManager.DataAccess.MSSQL.Entities.BusStop", "DepartureBusStop")
                        .WithMany("DepartureBusStopVoyages")
                        .HasForeignKey("DepartureBusStopId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ArrivalBusStop");

                    b.Navigation("DepartureBusStop");
                });

            modelBuilder.Entity("BusManager.DataAccess.MSSQL.Entities.BusStop", b =>
                {
                    b.Navigation("ArrivalBusStopVoyages");

                    b.Navigation("DepartureBusStopVoyages");
                });

            modelBuilder.Entity("BusManager.DataAccess.MSSQL.Entities.Order", b =>
                {
                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("BusManager.DataAccess.MSSQL.Entities.UserEntity", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("BusManager.DataAccess.MSSQL.Entities.VoyageInfo", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
