using System;

namespace Timelogger.DTOs
{
    public class TimeRegistrationDTO
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}