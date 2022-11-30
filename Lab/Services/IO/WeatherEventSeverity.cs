namespace Lab.Services.IO;
public enum WeatherEventSeverity
{
    Light = 0,
    Moderate = 1,
    Severe = 2,
    Heavy = 3,
    UNK = 4,
    Other = 5,
    
    Unknown = 100 // Created to process any other values of Severity.
}