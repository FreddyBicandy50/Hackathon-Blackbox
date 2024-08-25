using HackathonBackend.Comms;
using HackathonBackend.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace HackathonBackend.Controllers;

[Route("api/cicd")]
[ApiController]
public class ApiController : ControllerBase
{
	[HttpGet]
	public async Task<IActionResult> GetReport([FromQuery] string repo, [FromQuery] string prompt)
	{
		// If the Repo url is missing then we should return a badRequest
		if (string.IsNullOrWhiteSpace(repo))
		{
			return BadRequest("Github repository not valid");
		}

		// Retrieving the files inside the GitHub repo
		var gitResult = await GithubApi.SearchGit(repo);

		// If the gitResult is null then no problem Occured while fetching files from the GitHub repo
		if (gitResult is null)
		{
			var aiResponse = await GeminiApi.SendToAiAsync(prompt, await System.IO.File.ReadAllBytesAsync("Created/code.txt"));

			// In case the AI response is null or empty then the prompt or data sent is invalid 
			if (aiResponse is null)
			{
				return BadRequest("Error Analyzing Sent Data");
			}

			var report = ReportMapper.ToReport(aiResponse);

			if (report is null)
			{
				return NotFound("An error occured while generating response");
			}

			return Ok(report);
		}

		// If the GitHub repo url was not valid then an error message is returned which is then sent from the api as a badRequest
		return BadRequest(gitResult);
	}
}