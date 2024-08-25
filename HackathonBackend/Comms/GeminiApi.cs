using GenerativeAI.Classes;
using GenerativeAI.Models;
using GenerativeAI.Types;

namespace HackathonBackend.Comms;

public class GeminiApi
{
    private static readonly string _geminiToken = "AIzaSyDe1OJ8kf7CRZEpksrEPpaFym6BP61tyFY";
    /**
     * The instruction sent to the AI indicating what is asked from it
     */
    private static readonly string _instructions = "You are a senior developer and expert in CICD (continuous integration continuous delivery) especially in the c programming language. " +
                                                   "You will receive from the user a text file called code.txt which might contain the content of one or more .c or .h files, each file has its name stated before it in the following manner ...... filename ...... " +
                                                   "and a paragraph that could contain some explanation about the code or any information that may be relevant. " +
                                                   "Perform the following steps on the file code.txt: " +
                                                   "1-Act like a senior c developer. " +
                                                   "2-Debug this c code while checking for any memory leaks or errors and suggest some optimizations when " +
                                                   "necessary by rewriting the code that should be edited and add comments explaining in details before the parts that were changed. " +
                                                   "3-Provide a  summary for the code present in the code.txt file that was sent explaining what this code does. " +
                                                   "Output all the steps in the following manner: First you are required to return your response in a markup custom markup language using the following elements: " +
                                                   "text: this element will contain every text that is not c code like the summary or the explanations you will provide when necessary. " +
                                                   "code: this element will contain every c code that you will generate including the comments. " +
                                                   "Your answers should be the smallest possible especially in the code you should not rewrite the code unless there is an error or a problem that is present and in this case you should only rewrite the parts that needs to be fixed" +
                                                   "Now this is a simulation about a real live interaction with the user: " +
                                                   "user: This code is written to do .... "+
                                                   "ai: <text>summary</text>"+
                                                   "ai: <code> c code generate including the comment</code>" +
                                                   "ai: any other information either inside a text element or a code element depending on its nature";

    /**
     * Send the prompt from the user and the files retrieved from the GitHub to the AI API and returning the response.
     */
    public static async Task<string?> SendToAiAsync(string prompt, byte[] bytes)
    {
        // var model2 = new GenerativeModel(_geminiToken);
        
        var model = new Gemini15Flash(_geminiToken);

        // var response = await model2.GenerateContentAsync(new List<Part>(){new Part(){Text = _instructions + prompt, InlineData = new GenerativeContentBlob(){MimeType = "text/plain", Data = Convert.ToBase64String(bytes)}}});

        var codeText = await File.ReadAllTextAsync("Created/code.txt");

        var response =
            await model.GenerateContentAsync(_instructions + " User Prompt: " + prompt + " code.txt content: " +
                                             codeText);
        
        Console.WriteLine(response);
        return response;
    }
}