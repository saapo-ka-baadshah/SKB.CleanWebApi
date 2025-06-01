namespace SKB.App.Domain.GetWeatherForecast;

/// <summary>
/// Provides the business logic that provides the Weather Forecast
/// </summary>
public class GetWeatherForecastController
{
	private readonly string[] _summaries =
	[
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	];

	/// <summary>
	/// Provides the Weather Information
	/// </summary>
	/// <returns>Returns a <see cref="GetWeatherForecastTransmissionData"/> object</returns>
	public GetWeatherForecastTransmissionData[] GetWeatherForecast()
	{
		return Enumerable.Range(1, 5).Select(index =>
				new GetWeatherForecastTransmissionData
				(
					DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
					Random.Shared.Next(-20, 55),
					_summaries[Random.Shared.Next(_summaries.Length)]
				))
			.ToArray();
	}
}
