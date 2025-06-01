namespace SKB.App.Domain.GetWeatherForecast;

/// <summary>
/// Data transfer object for WeatherForecast information
/// </summary>
/// <param name="Date">Date for the target Weather Forecast</param>
/// <param name="TemperatureC">Temperature in Celsius as integer </param>
/// <param name="Summary">Optional Summary</param>
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
