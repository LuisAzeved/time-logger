using System.Collections.Generic;
using AutoMapper;
using Timelogger.App.Interfaces;
using Timelogger.DTOs;
using Timelogger.Enums;
using Timelogger.Repository.Interfaces;

namespace Timelogger.App.Features
{
    public class GetProjectsHandler: IGetProjectsHandler
    {
        
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetProjectsHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }
        
        public List<ProjectDTO> Handle(SortCriteria sortCriteria, string sortFieldName)
        {

            string fieldNameWithUpperLetter = null; 
            if (sortFieldName?.Length > 0)
            {
                 fieldNameWithUpperLetter = char.ToUpper(sortFieldName[0]) + sortFieldName.Substring(1);
            } 
            
            var projectList = _projectRepository.GetAll(sortCriteria, fieldNameWithUpperLetter ?? "Id");
            return _mapper.Map<List<ProjectDTO>>(projectList);
            
        }
        
        
    }
}