using System.Text.RegularExpressions;
using HackathonBackend.Models;

namespace HackathonBackend.Helpers;

public class ReportMapper
{
    /**
     * Takes the AI response which written as a markup language to e able to differentiate the text part form the code parts and returns a report object
     */
    public static Report? ToReport(string aiResponse)
    {
        var response = aiResponse.Replace("<|eot_id|>", "");
        
        var result = new Report();
        
        string pattern = @"<(?<tag>\w+)>.*?<\/\k<tag>>";

        var matches = Regex.Matches(response, pattern);

        // If the response does not correspond to a markup language then we should return a null which means an error occured
        if (matches.Count == 0)
        {
            Console.WriteLine("Nothing is there");
            return null;
        }
        
        foreach (Match match in matches)
        {
            string tag = match.Groups["tag"].Value;
            string text = Regex.Replace(match.Value, "<.*?>", string.Empty).Trim();

            // if (tag.Contains("code"))
            // {
            //     result.Items.Add(new ReportItem(){IsCode = true, Text = text});
            // }
            
            result.Items.Add(new ReportItem(){ IsCode = tag.Contains("code"), Text = text});
        }
        
        return result;
    }
}