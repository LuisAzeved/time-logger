using Microsoft.AspNetCore.Mvc;
using Timelogger.App.Features;
using Timelogger.App.Interfaces;
using Timelogger.DTOs;
using Timelogger.Repository;

namespace Timelogger.Api.Controllers
{
	[Route("api/[controller]")]
	public class ProjectsController : Controller
	{
		private readonly IGetProjectsHandler _getProjectsHandler;
		private readonly IGetProjectHandler _getProjectHandler;


		public ProjectsController(IGetProjectsHandler getProjectsHandler, IGetProjectHandler getProjectHandler)
		{
			_getProjectsHandler = getProjectsHandler;
			_getProjectHandler = getProjectHandler;
		}

		[HttpGet]
		[Route("hello-world")]
		public string HelloWorld()
		{
			return "Hello Back!";
		}

		// GET api/projects
		[HttpGet]
		public IActionResult GetAll(SortDTO sort)
		{
			return Ok(_getProjectsHandler.Handle(sort.SortCriteria, sort.SortFieldName));
		}

		[HttpGet]
		[Route("{projectId?}")]
		public IActionResult GetProject(int projectId)
		{
			return Ok(_getProjectHandler.Handle(projectId));
		}
	}
}
