using System;
using System.Collections.Generic;

namespace Timelogger.DTOs
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Deadline { get; set; }
        public ICollection<TimeRegistrationDTO> TimeRegistrations { get; set; }
    }
}