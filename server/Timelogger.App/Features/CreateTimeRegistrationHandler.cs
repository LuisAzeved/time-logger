using System;
using AutoMapper;
using Timelogger.App.Interfaces;
using Timelogger.DTOs;
using Timelogger.Entities;
using Timelogger.Repository.Interfaces;

namespace Timelogger.App.Features
{
    public class CreateTimeRegistrationHandler: ICreateTimeRegistrationHandler
    {
        private readonly ITimeRegistrationRepository _timeRegistrationRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;


        public CreateTimeRegistrationHandler(ITimeRegistrationRepository timeRegistrationRepository, IProjectRepository projectRepository, IMapper mapper)
        {
            _timeRegistrationRepository = timeRegistrationRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }
        
        public ResultDTO<TimeRegistrationDTO> Handle(TimeRegistrationDTO timeRegistrationDTO)
        {
            var result = new ResultDTO<TimeRegistrationDTO>();

            var project = _projectRepository.GetById(timeRegistrationDTO.ProjectId);
            if (project == null)
            {
                result.Success = false;
                result.ErrorMessage = "Project not found!";
                return result;
            }
            
            if (DateTime.Now > project.Deadline)
            {
                result.Success = false;
                result.ErrorMessage = "This project is already complete. No more time registrations accepted.";
                return result;
            }
            
            if (timeRegistrationDTO.End <= timeRegistrationDTO.Start)
            {
                result.Success = false;
                result.ErrorMessage = "End date should be after start date";
                return result;
            }

            if (timeRegistrationDTO.End.Subtract(timeRegistrationDTO.Start).TotalMinutes < 30)
            {
                result.Success = false;
                result.ErrorMessage = "Span between start and end must be 30 minutes or longer";
                return result;
            }
            
            if (timeRegistrationDTO.End > project.Deadline)
            {
                result.Success = false;
                result.ErrorMessage = "End date should be before project deadline";
                return result;
            }
            
            var timeRegistration = _mapper.Map<TimeRegistration>(timeRegistrationDTO);
            
            var timeRegistrationFromDb = _timeRegistrationRepository.CreateTimeRegistration(timeRegistration);
            
            result.Success = true;
            result.Data = _mapper.Map<TimeRegistrationDTO>(timeRegistrationFromDb);
            
            return result;
        }
    }
}