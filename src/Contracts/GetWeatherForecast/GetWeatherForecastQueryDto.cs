namespace SKB.App.Contracts.GetWeatherForecast;

/// <summary>
/// Data transfer object for WeatherForecast information
/// </summary>
public record GetWeatherForecastTransmissionData(
	DateOnly Date,
	int TemperatureC,
	string? Summary)
{
	/// <summary>
	/// Temperature in F
	/// </summary>
	public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

/// <summary>
/// Singular Entropy Dto for GetWeatherForecastQuery
/// </summary>
/// <param name="TransmissionData"></param>
/// <param name="Metadata"></param>
public record GetWeatherForecastQueryDto(
		GetWeatherForecastTransmissionData[] TransmissionData,
		params KeyValuePair<string, object?>[]? Metadata
	);
