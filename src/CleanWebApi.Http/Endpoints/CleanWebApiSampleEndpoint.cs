using MediatR;
using Microsoft.AspNetCore.Mvc;
using SKB.App.Contracts.GetWeatherForecast;
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

		app.MapGet("/weatherforecast", async (
				IMediator mediator
				) =>
			{
				var req = new GetWeatherForecastQuery();
				var responseDto = await mediator.Send(req);
				return responseDto;
			})
			.WithName("GetWeatherForecast")
			.WithSummary("This is a sample API Endpoint")
			.WithDescription("This is not supposed to part of your Production Stack");
	}
}
