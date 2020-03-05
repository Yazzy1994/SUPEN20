using AutoMapper;
using SUPEN20DB.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemAPI.Profiles
{
    public class CreditProfile : Profile
    {
        public CreditProfile()
        {
            CreateMap<Credit, Models.CreditDTO>()
                .ReverseMap();
        }
    }
}
