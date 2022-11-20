using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Text.RegularExpressions;


namespace Lab;

public static class Handler
{
    private static async Task<string> GetText(string uri)
    {
        var client = new HttpClient();
        var resp = await client.GetStringAsync(uri);

        return resp;
    }

    private static IReadOnlyList<string> GetLinks(string text)
    {
        var rg = new Regex("href=\"(.+?)\"");
        return rg.Matches(text).Select(x => x.Groups[1].Value).ToList();
    }

    public static async Task<IResult> RegexQuery(string uri)
    {
        if (uri is null or "")
        {
            return Results.BadRequest("Empty or null uri was given");
        }

        try
        {
            var text = await GetText(uri);
            return Results.Ok(GetLinks(text));
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    public static IResult Random(int length, string source)
    {
        var lettersSet = new HashSet<char>(source.ToArray());

        if (lettersSet.Count != source.Length)
        {
            return Results.BadRequest("Среди переданных символов были дубликаты!");
        }

        if (length < 1)
        {
            return Results.BadRequest("Длина слов должна быть положительным числом!");
        }

        var res = new List<string>();
        var letters = new string(lettersSet.ToArray());
        for (int i = 0; i < length; i++)
        {
            var sb = new StringBuilder();
            for (int j = 0; j < length; j++)
            {
                sb.Append(letters[new Random().Next(0, letters.Length)]);
            }

            res.Add(sb.ToString());
        }

        return Results.Ok(res);
    }
}