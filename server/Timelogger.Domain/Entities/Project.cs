using System;
using System.Collections.Generic;

namespace Timelogger.Entities
{
	public class Project
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime Deadline { get; set; }
		public ICollection<TimeRegistration> TimeRegistrations { get; set; }
	}
}
