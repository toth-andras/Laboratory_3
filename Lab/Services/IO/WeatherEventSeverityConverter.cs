using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Lab.Services.IO;

public class WeatherEventSeverityConverter : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        return text switch
        {
            "Light" => WeatherEventSeverity.Light,
            "Moderate" => WeatherEventSeverity.Moderate,
            "Severe" => WeatherEventSeverity.Severe,
            "Heavy" => WeatherEventSeverity.Heavy,
            "UNK" => WeatherEventSeverity.UNK,
            "Other" => WeatherEventSeverity.Other,
            
            _ => WeatherEventSeverity.Unknown
        };
    }
}