using System.Text.Json;
using System.Text.Json.Nodes;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Lab.Services.IO;

public class WeatherEventTypeConverter : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        return text switch
        {
            "Snow" => WeatherEventType.Snow,
            "Fog" => WeatherEventType.Fog,
            "Rain" => WeatherEventType.Rain,
            "Cold" => WeatherEventType.Cold,
            "Storm" => WeatherEventType.Storm,
            
            _ => WeatherEventType.Unknown
        };
    }
}