using AutoMapper;
using Timelogger.App.Interfaces;
using Timelogger.DTOs;
using Timelogger.Repository.Interfaces;

namespace Timelogger.App.Features
{
    public class GetProjectHandler: IGetProjectHandler
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetProjectHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }
        
        public ProjectDTO Handle(int projectId)
        {
            var project =  _projectRepository.GetById(projectId);
            return _mapper.Map<ProjectDTO>(project);
        }
    }
}