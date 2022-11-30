using CsvHelper.Configuration.Attributes;

namespace Lab.Services.IO;


/// <summary>
/// Represents the model of weather event described in a .csv file.
/// </summary>
public class WeatherEventModel
{
    public string EventId { get; set; }
    
    [TypeConverter(typeof(WeatherEventTypeConverter))]
    public WeatherEventType Type { get; set; }
    
    [TypeConverter(typeof(WeatherEventSeverityConverter))]
    public WeatherEventSeverity Severity { get; set; }
    
    [Name("StartTime(UTC)")]
    public DateTime StartTimeUTC { get; set; }
    
    [Name("EndTime(UTC)")]
    public DateTime EndTimeUTC { get; set; }
    
    [Name("Precipitation(in)")]
    public double Precipitation { get; set; }
    
    public string TimeZone { get; set; }
    
    public string AirportCode { get; set; }
    
    public double LocationLat { get; set; }
    
    public double LocationLng { get; set; }
    
    public string City { get; set; }
    
    public string County { get; set; }
    
    public string State { get; set; }
    
    [TypeConverter(typeof(ZipCodeConverter))]
    public int ZipCode { get; set; }
}