using Timelogger.DTOs;

namespace Timelogger.App.Interfaces
{
    public interface IGetProjectHandler
    {
        ProjectDTO Handle(int projectId);
    }
}