using AutoMapper;
using BusManager.Application.Contracts.BusStop;
using BusManager.Application.Contracts.Ticket;
using BusManager.Application.Contracts.Voyage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusManager.Tests
{
    public class SourceMappingProfile : Profile
    {
        public SourceMappingProfile()
        {
            CreateMap<BusStopRequest, Domain.Models.BusStop>();

            CreateMap<Domain.Models.BusStop, BusStopRequest>();

            CreateMap<VoyageInfoRequest, Domain.Models.VoyageInfo>();
            CreateMap<Domain.Models.VoyageInfo, VoyageInfoRequest>();

            CreateMap<Domain.Models.Ticket, TicketRepsonse>();
            CreateMap<TicketRepsonse, Domain.Models.Ticket>();

            CreateMap<DataAccess.MSSQL.Entities.BusStop, Domain.Models.BusStop>().ReverseMap();
            CreateMap<DataAccess.MSSQL.Entities.UserEntity, Domain.Models.User>().ReverseMap();
            CreateMap<DataAccess.MSSQL.Entities.Order, Domain.Models.Order>().ReverseMap();
            CreateMap<DataAccess.MSSQL.Entities.Ticket, Domain.Models.Ticket>().ReverseMap();
            CreateMap<DataAccess.MSSQL.Entities.VoyageInfo, Domain.Models.VoyageInfo>().ReverseMap();
        }


    }
}
