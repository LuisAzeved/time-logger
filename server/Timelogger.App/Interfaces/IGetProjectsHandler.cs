using System.Collections.Generic;
using Timelogger.DTOs;
using Timelogger.Enums;

namespace Timelogger.App.Interfaces
{
    public interface IGetProjectsHandler
    {
        List<ProjectDTO> Handle(SortCriteria sortCriteria, string sortFieldName);
    }
}