using Timelogger.Entities;

namespace Timelogger.Repository.Interfaces
{
    public interface ITimeRegistrationRepository
    {
        TimeRegistration CreateTimeRegistration(TimeRegistration timeRegistration);
    }
}