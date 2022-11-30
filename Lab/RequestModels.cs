using Microsoft.AspNetCore.Mvc;

namespace Lab;

/// <summary>
/// Db work.
/// </summary>
/// <param name="FilePath">A path to the file with csv data.</param>
public record InitializeRequestModel(string FilePath);