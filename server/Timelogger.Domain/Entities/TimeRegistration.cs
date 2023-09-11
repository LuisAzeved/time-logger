using System;

namespace Timelogger.Entities
{
	public class TimeRegistration
	{
		public int Id { get; set; }
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
		public int ProjectId { get; set; }
	}
}
