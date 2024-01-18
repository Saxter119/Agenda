using Agenda.BusinessLogic.dtos;
using Agenda.BusinessLogic.entities;
using Agenda.DataAccess.entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Agenda.BusinessLogic.services
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Phone, PhoneDto>()
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number));

            CreateMap<Email, EmailDto>()
            .ForMember(dest => dest.EmailContact, opt => opt.MapFrom(src => src.EmailAddress));
        }

    }
}
