using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Timelogger.Entities;
using Timelogger.Enums;
using Timelogger.Repository.Interfaces;

namespace Timelogger.Repository.Repositories
{
    public class ProjectRepository: IProjectRepository
    {
        
        private readonly DBContext _db;

        public ProjectRepository(DBContext context)
        {
            _db = context;
        }

        public Project GetById(int projectId)
        {
             return _db.Projects
                .Include(p => p.TimeRegistrations)
                .FirstOrDefault(p => p.Id == projectId);
        }
        
        public List<Project> GetAll(SortCriteria sortCriteria = SortCriteria.NONE, string sortFieldName = "id")
        { 
            var projects = _db.Projects
                .Include(p => p.TimeRegistrations)
                .AsQueryable();
            
            switch (sortCriteria)
            {
                case SortCriteria.ASC:
                    projects = projects.OrderBy(p => EF.Property<object>(p, sortFieldName));
                    break;
                case SortCriteria.DESC:
                    projects = projects.OrderByDescending(p => EF.Property<object>(p, sortFieldName));
                    break;
            }

            return projects.ToList();
        }
    }
}