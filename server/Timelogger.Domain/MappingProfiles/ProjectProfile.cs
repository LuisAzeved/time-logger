
using AutoMapper;
using Timelogger.DTOs;
using Timelogger.Entities;

namespace Timelogger.MappingProfiles
{
    
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDTO>().ReverseMap();
        }
    }
    
}