using SKB.Core.Abstractions.WebApi;

namespace SKB.App.CleanWebApi.Http.Endpoints;

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
	public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

/// <summary>
/// CleanWebApi's Sample Endpoint to demonstrate the Functionalities
/// </summary>
public class CleanWebApiSampleEndpoint: IBaseEndpoint
{
	/// <inheritdoc/>
	public void RegisterAllMethods(IEndpointRouteBuilder app)
	{
		var summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		app.MapGet("/weatherforecast", () =>
			{
				var forecast = Enumerable.Range(1, 5).Select(index =>
						new WeatherForecast
						(
							DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
							Random.Shared.Next(-20, 55),
							summaries[Random.Shared.Next(summaries.Length)]
						))
					.ToArray();
				return forecast;
			})
			.WithName("GetWeatherForecast")
			.WithSummary("This is a sample API Endpoint")
			.WithDescription("This is not supposed to part of your Production Stack");
	}
}
