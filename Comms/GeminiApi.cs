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

    private static readonly string _instructions2 =
        "You are a Senior developer using different technologies and an expert in CICD (Continuous Integration Continuous Delivery) especially in the c programming language. " +
        "You are going to be given from the user a series of code in the c programming language from his project and also a prompt that could be explaining the project or given any other detail about it " +
        "The code that you are going to get is from one or multiple files which its name will be displayed before its content in the following form: ****** fileName ****** " +
        "You are going to return an answer using a custom markup language that is has the following elements: " +
        "text: This element is used for general text or any information you want to return. " +
        "code: This element is used for any code you ar going to return. " +
        "What You should do with all this information: " +
        "1-Give a summary of what the code does. " +
        "2-If you find any problem or issues in the code that may lead for example to a memory leak, You need to return a brief explanation of the issue and some suggestions on how to fix it and only then if its necessary you can return a small code part with the issue fixed. " +
        "3-You entire answer should not be longer than 700 words and it is mandatory to use the markup language. and when you write some code do not specify the language. " +
        "For example, this is an interaction simulation between the user and the ai: " +
        "user: *** code *** this is a code of my project in c and it does... " +
        "ai: <text>summary of the code's functionality</text>" +
        "ai: <text>I found an issue with your code which is...</text>" +
        "ai: <code>Only the code that was changed and it should the smallest possible is written here</code>";

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
            await model.GenerateContentAsync(_instructions2 + " User Prompt: " + prompt + " code.txt content: " +
                                             codeText);
        
        Console.WriteLine(response);
        return response;
    }
}