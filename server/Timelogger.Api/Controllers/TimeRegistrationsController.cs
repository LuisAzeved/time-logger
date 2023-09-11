using Microsoft.AspNetCore.Mvc;
using Timelogger.App.Features;
using Timelogger.App.Interfaces;
using Timelogger.DTOs;

namespace Timelogger.Api.Controllers
{
	[Route("api/[controller]")]
	public class TimeRegistrationsController : Controller
	{
		private readonly ICreateTimeRegistrationHandler _createTimeRegistrationHandler;

		public TimeRegistrationsController(ICreateTimeRegistrationHandler createTimeRegistrationHandler)
		{
			_createTimeRegistrationHandler = createTimeRegistrationHandler;
		}

		[HttpPost]
		[Route("create")]
		public IActionResult CreateTimeRegistration([FromBody] TimeRegistrationDTO timeRegistrationDTO)
		{

			var result = _createTimeRegistrationHandler.Handle(timeRegistrationDTO);
			if (result.Success)
			{
				return Ok(result.Data);
			}
			
			return BadRequest(new { error = result.ErrorMessage });
		}


	}
}
