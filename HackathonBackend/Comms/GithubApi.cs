using Octokit;

namespace HackathonBackend.Comms;

public class GithubApi
{

    public static async void SearchGit(string owner, string repo)
    {
        var github = new GitHubClient(new ProductHeaderValue("Asp.net-Hackathon"))
        {
            Credentials = new Credentials("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")
        };
        
        string fileOutputPath = Path.Combine("Created", $"code.txt");
        await File.WriteAllTextAsync(fileOutputPath, "");
        
        await SearchGitAndWriteFile(owner, repo, "c", fileOutputPath, github);
        await SearchGitAndWriteFile(owner, repo, "h", fileOutputPath, github);
    }

    private static async Task SearchGitAndWriteFile(string owner, string repo, string extension, string fileOutputPath, 
        GitHubClient github)
    {
        string query = $"repo:{owner}/{repo} extension:{extension}";

        var searchRequest = new SearchCodeRequest(query);
        var searchResult = await github.Search.SearchCode(searchRequest);

        if (searchResult.Items.Any())
        {
            foreach (var file in searchResult.Items)
            {
                Console.WriteLine($"File: {file.Path} (SHA: {file.Sha})");
                
                var contents = 
                    await github.Repository.Content.GetAllContents(owner, repo, file.Path);
                
                string fileName = Path.GetFileName(file.Path);

                string separator = $"\n****** {fileName} ******\n";
                await File.AppendAllTextAsync(fileOutputPath, separator);

                await File.AppendAllTextAsync(fileOutputPath, contents[0].Content);
                
                Console.WriteLine($"Written file {fileName} to {fileOutputPath}");
            }
        }
        else
        {
            Console.WriteLine("No File Found");
        }
    }
}