using AutoMapper;
using BusManager.DataAccess.MSSQL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusManager.DataAccess.MSSQL
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<Entities.BusStop, Domain.Models.BusStop>().ReverseMap();
            CreateMap<Entities.UserEntity, Domain.Models.User>().ReverseMap();
            CreateMap<Entities.Order, Domain.Models.Order>().ReverseMap();
            CreateMap<Entities.Ticket, Domain.Models.Ticket>().ReverseMap();
            CreateMap<Entities.VoyageInfo, Domain.Models.VoyageInfo>().ReverseMap();
        }
    }
}
