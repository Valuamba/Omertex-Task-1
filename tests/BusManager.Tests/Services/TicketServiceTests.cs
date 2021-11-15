using BusManager.Application.Services;
using BusManager.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BusManager.Domain.Models;

namespace BusManager.Tests.Services
{
    public class TicketServiceTests : BaseUnitTest
    {
        public TicketServiceTests() : base()
        {
        }

        [Fact]
        public async Task Should_BuyBackTicket()
        {
            //arrange
            Mock<ITicketRepository> ticketRepMock = new();
            Mock<IVoyagesRepository> voyagesRepMock = new();
            Mock<IOrderRepository> ordersRepMock = new();

            int ticketId = 14;
            int voyageId = 1;
            Ticket callbackTicket = null;

            ticketRepMock.Setup(t => t.UpdateTicket(It.IsAny<Ticket>()))
                .Callback<Ticket>(c => callbackTicket = c);

            voyagesRepMock.Setup(v => v.GetVoyageByIdAsync(voyageId))
                .Returns(Task.FromResult(new VoyageInfo()
                {
                    DepartureDateTime = DateTime.Now.AddMinutes(1)
                }));

            ticketRepMock.Setup(t => t.GetTicketByIdAsync(ticketId))
                .Returns(Task.FromResult(new Ticket()
                {
                    PassengerDocumentNumber = "AB2312512",
                    PassengerFirstName = "sgrsg",
                    PassengerLastName = "sgdbdfb",
                    PassengerSeatNumber = 12,
                    Status = TicketStatus.Reserved,
                    Order = new Order()
                    {
                        Status = OrderStatus.Reserved,
                        VoyageId = voyageId
                    }
                }));

            //act
            var service = new TicketService(ticketRepMock.Object, _mapper, voyagesRepMock.Object, ordersRepMock.Object);
            
            await service.BuyBackReservedTicket(ticketId);

            //assert
            Assert.Equal(TicketStatus.BoughtOut, callbackTicket.Status);
            Assert.Equal(OrderStatus.BoughtOut, callbackTicket.Order.Status);
        }

        [Fact]
        public async Task Should_NotAllow_BuyBackTicket_WrongTicketStatus()
        {
            //arrange
            Mock<ITicketRepository> ticketRepMock = new();
            Mock<IVoyagesRepository> voyagesRepMock = new();
            Mock<IOrderRepository> ordersRepMock = new();

            int ticketId = 14;
            int voyageId = 1;
            Ticket callbackTicket = null;

            ticketRepMock.Setup(t => t.UpdateTicket(It.IsAny<Ticket>()))
                .Callback<Ticket>(c => callbackTicket = c);

            voyagesRepMock.Setup(v => v.GetVoyageByIdAsync(voyageId))
                .Returns(Task.FromResult(new VoyageInfo()
                {
                    DepartureDateTime = DateTime.Now.AddMinutes(1)
                }));

            ticketRepMock.Setup(t => t.GetTicketByIdAsync(ticketId))
                .Returns(Task.FromResult(new Ticket()
                {
                    PassengerDocumentNumber = "AB2312512",
                    PassengerFirstName = "sgrsg",
                    PassengerLastName = "sgdbdfb",
                    PassengerSeatNumber = 12,
                    Status = TicketStatus.BoughtOut,
                    Order = new Order()
                    {
                        Status = OrderStatus.Reserved,
                        VoyageId = voyageId
                    }
                }));

            //act
            var service = new TicketService(ticketRepMock.Object, _mapper, voyagesRepMock.Object, ordersRepMock.Object);

            //assert
            var exception = await Assert.ThrowsAsync<Exception>(() => service.BuyBackReservedTicket(ticketId));
            Assert.Equal("The ticket should have status Reserved.", exception.Message);
        }

        [Fact]
        public async Task Should_NotAllow_BuyBackTicket_WrongOrderStatus()
        {
            //arrange
            Mock<ITicketRepository> ticketRepMock = new();
            Mock<IVoyagesRepository> voyagesRepMock = new();
            Mock<IOrderRepository> ordersRepMock = new();

            int ticketId = 14;
            int voyageId = 1;
            Ticket callbackTicket = null;

            ticketRepMock.Setup(t => t.UpdateTicket(It.IsAny<Ticket>()))
                .Callback<Ticket>(c => callbackTicket = c);

            voyagesRepMock.Setup(v => v.GetVoyageByIdAsync(voyageId))
                .Returns(Task.FromResult(new VoyageInfo()
                {
                    DepartureDateTime = DateTime.Now.AddMinutes(1)
                }));

            ticketRepMock.Setup(t => t.GetTicketByIdAsync(ticketId))
                .Returns(Task.FromResult(new Ticket()
                {
                    PassengerDocumentNumber = "AB2312512",
                    PassengerFirstName = "sgrsg",
                    PassengerLastName = "sgdbdfb",
                    PassengerSeatNumber = 12,
                    Status = TicketStatus.Reserved,
                    Order = new Order()
                    {
                        Status = OrderStatus.BoughtOut,
                        VoyageId = voyageId
                    }
                }));

            //act
            var service = new TicketService(ticketRepMock.Object, _mapper, voyagesRepMock.Object, ordersRepMock.Object);

            //assert
            var exception = await Assert.ThrowsAsync<Exception>(() => service.BuyBackReservedTicket(ticketId));
            Assert.Equal("The order should have status Reserved.", exception.Message);
        }
    }
}
