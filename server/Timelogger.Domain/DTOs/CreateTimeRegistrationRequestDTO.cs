using System;

namespace Timelogger.DTOs
{
    public class CreateTimeRegistrationRequestDTO
    {
        public string ProjectId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}