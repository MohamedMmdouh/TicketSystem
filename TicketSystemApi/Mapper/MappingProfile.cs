using AutoMapper;
using System;
using TicketSystemApi.Models;
using TicketSystemApi.Persistance.Data;

namespace TicketSystemApi.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterModel, User>().ForMember(m => m.UserName, D => D.MapFrom(m => m.Username))
                                             .ForMember(m => m.Email, d => d.MapFrom(m => m.Email))
                                             .ForMember(m => m.EmailConfirmed, d => d.MapFrom(m => true))
                                             .ForMember(m => m.LockoutEnabled, d => d.MapFrom(m => false))
                                             .ForMember(m => m.SecurityStamp, d => d.MapFrom(m => Guid.NewGuid().ToString()));

            CreateMap<TicketViewModel, Ticket>().ForMember(m => m.StartTime, d => d.MapFrom(m => Convert.ToDateTime(m.StartTime).TimeOfDay))
                                                .ForMember(m => m.EndTime, d => d.MapFrom(m => Convert.ToDateTime(m.EndTime).TimeOfDay));

            CreateMap<Ticket, TicketViewModel>().ForMember(m => m.StartTime, d => d.MapFrom(m => m.StartTime.ToString()))
                                                .ForMember(m => m.EndTime, d => d.MapFrom(m => m.EndTime.ToString()));
        }

    }
}
