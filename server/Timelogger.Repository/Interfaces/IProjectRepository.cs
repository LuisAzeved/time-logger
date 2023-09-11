using System.Collections.Generic;
using Timelogger.Entities;
using Timelogger.Enums;

namespace Timelogger.Repository.Interfaces
{
    public interface IProjectRepository
    {
        Project GetById(int projectId);
        List<Project> GetAll(SortCriteria sortCriteria, string sortFieldName);
    }
}