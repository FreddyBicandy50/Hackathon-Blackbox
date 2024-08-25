using HackathonBackend.Comms;
using HackathonBackend.Helpers;
using HackathonBackend.Models;
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

	[HttpGet("test")]
	public IActionResult Test()
	{
		var testReport = new Report()
		{
			Items = new List<ReportItem>()
			{
				new ReportItem() { IsCode = false, Text = "Test text to be displayed" },
				new ReportItem() { IsCode = true, Text = "public static void main(String[] args)" }
			}
		};
		
		return Ok(testReport);
	}

	[HttpGet("geminiTest")]
	public async Task<IActionResult> TestAi()
	{
		var aiResp = await GeminiApi.SendToAiAsync("This code is my try to recreate a library containing some basic operations and function of the c language", await System.IO.File.ReadAllBytesAsync("Created/Code.txt"));
		
		Console.WriteLine(aiResp);
		return Ok(aiResp);
	}
}