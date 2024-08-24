using Octokit;

namespace HackathonBackend.Comms;

public class GithubApi
{

    /**
     * Search a GitHub repo for files with the .c and .h extensions and writes their content to a file in the Created folder called code.txt using the repo url
     */
    public static async Task<string?> SearchGit(string repoUrl)
    {
        // Convert the url string to uri and splits it to retrieve the name of the owner and the repo
        var uri = new Uri(repoUrl);
        var segments = uri.AbsolutePath.Split('/', StringSplitOptions.RemoveEmptyEntries);

        if (segments.Length < 2)
        {
            return "Invalid github url";
        }

        var owner = segments[0];
        var repo = segments[1];
        
        // in order to search a GitHub repo, we need a GitHub account credentials. For now, I am using my own,
        // but later we will create a login system that can be linked to a GitHub account
        var github = new GitHubClient(new ProductHeaderValue("Asp.net-Hackathon"))
        {
            Credentials = new Credentials("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")
        };
        
        string fileOutputPath = Path.Combine("Created", $"code.txt");
        await File.WriteAllTextAsync(fileOutputPath, "");
        
        await SearchGitAndWriteFile(owner, repo, "c", fileOutputPath, github);
        await SearchGitAndWriteFile(owner, repo, "h", fileOutputPath, github);
        
        return null;
    }

    /**
     * Search for files with a certain extension for example .c or .h or .exe in a GitHub repo by passing the owner and the name of the repo and the extension
     */
    private static async Task SearchGitAndWriteFile(string owner, string repo, string extension, string fileOutputPath, 
        GitHubClient github)
    {
        // This is the query used to search for specific file extensions in a database
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