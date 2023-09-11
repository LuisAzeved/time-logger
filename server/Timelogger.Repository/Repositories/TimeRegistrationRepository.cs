using System;
using Timelogger.Entities;
using Timelogger.Repository.Interfaces;

namespace Timelogger.Repository.Repositories
{
    public class TimeRegistrationRepository: ITimeRegistrationRepository
    {
        private readonly DBContext _db;

        public TimeRegistrationRepository(DBContext context)
        {
            _db = context;
        }

        public TimeRegistration CreateTimeRegistration(TimeRegistration timeRegistration)
        {
            var createdTimeRegistration = _db.TimeRegistrations.Add(timeRegistration);
            _db.SaveChanges();

            return createdTimeRegistration.Entity;
        }
    }
}