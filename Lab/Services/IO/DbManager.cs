namespace Lab.Services.IO;

using System.Data;
using Microsoft.Data.Sqlite;
using System.Linq;
using Dapper;

/// <summary>
/// Here all the logic for working with database is incapsulated.
/// </summary>
public class DbManager
{
    private const string EventsTableName = "weatherEvents";
    private const string EventTypeTableName = "eventTypes";
    private const string EventSeverityTableName = "eventSeverities";

    private readonly string _dataBaseName;
    private readonly string _connectionString;

    public DbManager(string dataBaseName)
    {
        _dataBaseName = dataBaseName;
        _connectionString = $"Data Source={dataBaseName}";
    }

    /// <summary>
    /// Gets the text value and the integer code of each value of enum.
    /// </summary>
    private IEnumerable<(string text, int value)> ParseEnum(Enum e)
    {
        return (from object? value in e.GetType().GetEnumValues() select (value.ToString(), (int) value)!);
    }

    /// <summary>
    /// Saves the enum to db.
    /// </summary>
    private void SaveEnum(Enum e, string tableName)
    {
        using var conn = new SqliteConnection(_connectionString);
        foreach (var enumValue in ParseEnum(e))
        {
            conn.Execute($"INSERT INTO {tableName} (id, TextValue) VALUES (@id, @TextValue)",
                new { id = enumValue.value, TextValue = enumValue.text});
        }
    }
    
    /// <summary>
    /// Saves the values of enums into db to normalize the data.
    /// </summary>
    private void Normalize()
    {
        SaveEnum(WeatherEventType.Cold, EventTypeTableName);
        SaveEnum(WeatherEventSeverity.Heavy, EventSeverityTableName);
    }
    
    /// <summary>
    /// Removes all the data stored in the database.
    /// </summary>
    private void ClearDataBase()
    {
        using var conn = new SqliteConnection(_connectionString);
        conn.Execute($"DELETE FROM {EventsTableName}");
        
        // Remove types and severities in case new ones were added.
        conn.Execute($"DELETE FROM {EventTypeTableName}");
        conn.Execute($"DELETE FROM {EventSeverityTableName}");
    }

    /// <summary>
    /// Creates the file of database and the tables needed.
    /// </summary>
    private async Task CreateDataBase()
    {
        using var conn = new SqliteConnection(_connectionString);
        await conn.ExecuteAsync("CREATE TABLE 'eventSeverities' ('id'	INTEGER,'TextValue'	TEXT,PRIMARY KEY('id'))");
        await conn.ExecuteAsync("CREATE TABLE 'eventTypes' ('id'	INTEGER,'TextValue'	TEXT,PRIMARY KEY('id'))");
        await conn.ExecuteAsync("CREATE TABLE 'weatherEvents' ('EventId' TEXT, 'Type' INTEGER, 'Severity' INTEGER, 'StartTimeUTC' TEXT, 'EndTimeUTC' TEXT, 'Precipitation' REAL, 'TimeZone' TEXT, 'AirportCode' TEXT, 'LocationLat' REAL, 'LocationLng' REAL, 'City' TEXT, 'County' TEXT, 'State' TEXT, 'ZipCode' INTEGER, PRIMARY KEY('EventId'))");
    }

    public async Task SaveToDb(IEnumerable<WeatherEventModel> events)
    {
        if (File.Exists($"{_dataBaseName}") is false)
        {
            await CreateDataBase();
        }
        else
        {
            ClearDataBase();
        }
        Normalize();
        
        using var conn = new SqliteConnection(_connectionString);
        //foreach (var weaterEvent in events)
        //{
            conn.Execute(
                $"INSERT INTO {EventsTableName} (EventId, Type, Severity, StartTimeUTC, EndTimeUTC, Precipitation, TimeZone, AirportCode, LocationLat, LocationLng, City, County, State, ZipCode)" +
                $" VALUES (@EventId, @Type, @Severity, @StartTimeUTC, @EndTimeUTC, @Precipitation, @TimeZone, @AirportCode, @LocationLat, @LocationLng, @City, @County, @State, @ZipCode)", events);
        //}
    }   
}