using Timelogger.DTOs;

namespace Timelogger.App.Interfaces
{
    public interface ICreateTimeRegistrationHandler
    {
        ResultDTO<TimeRegistrationDTO> Handle(TimeRegistrationDTO timeRegistrationDTO);

    }
}