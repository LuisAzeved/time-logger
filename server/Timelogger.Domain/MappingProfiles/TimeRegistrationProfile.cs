using AutoMapper;
using Timelogger.DTOs;
using Timelogger.Entities;

namespace Timelogger.MappingProfiles
{
    public class TimeRegistrationProfile : Profile
    {

            public TimeRegistrationProfile()
            {
                CreateMap<TimeRegistration, TimeRegistrationDTO>().ReverseMap();
            }
        
    }
}