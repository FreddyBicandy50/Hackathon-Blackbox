using Microsoft.AspNetCore.Mvc;

namespace HackathonBackend.Controllers;

[Route("api/cicd")]
[ApiController]
public class ApiController : ControllerBase
{
	[HttpGet]
	public IActionResult GetAll()
	{
		return Ok("Hello There");
	}
}