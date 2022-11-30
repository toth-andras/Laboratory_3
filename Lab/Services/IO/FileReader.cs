using System.Globalization;
using CsvHelper;

namespace Lab.Services.IO;

/// <summary>
/// Reds the data from the given .csv file.
/// </summary>
public static class FileReader
{
    public static IEnumerable<WeatherEventModel> GetData(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        
        return csv.GetRecords<WeatherEventModel>().ToList();
    }
}