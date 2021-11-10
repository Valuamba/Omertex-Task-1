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
            CreateMap<UserEntity, Domain.Models.User>().ReverseMap();
        }
    }
}
