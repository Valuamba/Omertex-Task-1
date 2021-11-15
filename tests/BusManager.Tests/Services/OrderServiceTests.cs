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
using BusManager.Application.Contracts.Order;

namespace BusManager.Tests.Services
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task Should_NotProcessOrder_SeatAlreadyTaken()
        {
            //arrange
            Mock<ITicketRepository> ticketRepMock = new();
            Mock<IOrderRepository> orderRepMock = new();
            Mock<IVoyagesRepository> voyagesRepMock = new();

            int userId = 13;
            int voyageId = 2;
            int passenegerSeatNumber = 13;

            orderRepMock.Setup(o => o.GetOrdersByVoyageIdAsync(voyageId))
                .Returns(new List<Order>()
                {
                    new Order() { Ticket = new() { PassengerSeatNumber = passenegerSeatNumber } }
                });

            var ticketOrder = new TicketOrder()
            {
                PassengerDocumentNumber = "AB2312512",
                PassengerFirstName = "Igor",
                PassengerLastName = "Miranchuk",
                PassengerSeatNumber = passenegerSeatNumber,
                TicketStatus = TicketStatus.Reserved,
                VoyageId = voyageId
            };

            //act
            var service = new OrderService(orderRepMock.Object, ticketRepMock.Object, voyagesRepMock.Object);

            //assert
            var exception = await Assert.ThrowsAsync<Exception>(() => service.ProcessOrder(userId, ticketOrder));
            Assert.Equal("The passenger seat already reserved.", exception.Message);
        }

        [Fact]
        public async Task Should_NotProcessOrder_IncorrectFullName()
        {
            //arrange
            Mock<ITicketRepository> ticketRepMock = new();
            Mock<IOrderRepository> orderRepMock = new();
            Mock<IVoyagesRepository> voyagesRepMock = new();

            int userId = 13;
            int voyageId = 2;
            int passenegerSeatNumber = 13;

            orderRepMock.Setup(o => o.GetOrdersByVoyageIdAsync(voyageId))
                .Returns(new List<Order>()
                {
                    new Order() { Ticket = new() { PassengerSeatNumber = 14 } }
                });

            var ticketOrder = new TicketOrder()
            {
                PassengerDocumentNumber = "AB2312512",
                PassengerFirstName = "",
                PassengerLastName = "",
                PassengerSeatNumber = passenegerSeatNumber,
                TicketStatus = TicketStatus.Reserved,
                VoyageId = voyageId
            };

            //act
            var service = new OrderService(orderRepMock.Object, ticketRepMock.Object, voyagesRepMock.Object);

            //assert
            var exception = await Assert.ThrowsAsync<Exception>(() => service.ProcessOrder(userId, ticketOrder));
            Assert.Equal("Incorrect first name or last name.", exception.Message);
        }

        [Fact]
        public async Task Should_NotProcessOrder_OutdatedVoyage()
        {
            //arrange
            Mock<ITicketRepository> ticketRepMock = new();
            Mock<IOrderRepository> orderRepMock = new();
            Mock<IVoyagesRepository> voyagesRepMock = new();

            int userId = 13;
            int voyageId = 2;
            int passenegerSeatNumber = 13;

            voyagesRepMock.Setup(v => v.GetVoyageByIdAsync(voyageId))
                .Returns(Task.FromResult(new VoyageInfo()
                {
                    DepartureDateTime = DateTime.Parse("11/03/2021 14:00:00")
                }));

            orderRepMock.Setup(o => o.GetOrdersByVoyageIdAsync(voyageId))
                .Returns(new List<Order>()
                {
                    new Order() { Ticket = new() { PassengerSeatNumber = 14 } }
                });

            var ticketOrder = new TicketOrder()
            {
                PassengerDocumentNumber = "AB2312512",
                PassengerFirstName = "sgrsg",
                PassengerLastName = "sgdbdfb",
                PassengerSeatNumber = passenegerSeatNumber,
                TicketStatus = TicketStatus.Reserved,
                VoyageId = voyageId
            };

            //act
            var service = new OrderService(orderRepMock.Object, ticketRepMock.Object, voyagesRepMock.Object);

            //assert
            var exception = await Assert.ThrowsAsync<Exception>(() => service.ProcessOrder(userId, ticketOrder));
            Assert.Equal("The voyage was outdated.", exception.Message);
        }

        [Fact]
        public async Task Should_ProcessOrder_Valid()
        {
            //arrange
            Mock<ITicketRepository> ticketRepMock = new();
            Mock<IOrderRepository> orderRepMock = new();
            Mock<IVoyagesRepository> voyagesRepMock = new();

            int userId = 13;
            int voyageId = 2;
            int passenegerSeatNumber = 13;

            voyagesRepMock.Setup(v => v.GetVoyageByIdAsync(voyageId))
                .Returns(Task.FromResult(new VoyageInfo()
                {
                    DepartureDateTime = DateTime.Parse("12/03/2021 14:00:00")
                }));

            orderRepMock.Setup(o => o.GetOrdersByVoyageIdAsync(voyageId))
                .Returns(new List<Order>()
                {
                    new Order() { Ticket = new() { PassengerSeatNumber = 14 } }
                });

            orderRepMock.Setup(o => o.AddOrder(It.IsAny<Order>()))
                .Returns(Task.FromResult(new Order() {  OrderId = 12 }));

            ticketRepMock.Setup(t => t.AddTicket(It.IsAny<Ticket>()))
               .Returns(Task.FromResult(new Ticket() { TicketId = 12 }));

            var ticketOrder = new TicketOrder()
            {
                PassengerDocumentNumber = "AB2312512",
                PassengerFirstName = "sgrsg",
                PassengerLastName = "sgdbdfb",
                PassengerSeatNumber = passenegerSeatNumber,
                TicketStatus = TicketStatus.Reserved,
                VoyageId = voyageId
            };

            //act
            var service = new OrderService(orderRepMock.Object, ticketRepMock.Object, voyagesRepMock.Object);
            await service.ProcessOrder(userId, ticketOrder);

            //assert
            ticketRepMock.Verify(t => t.AddTicket(It.IsAny<Ticket>()), Times.Once);
        }
    }
}
