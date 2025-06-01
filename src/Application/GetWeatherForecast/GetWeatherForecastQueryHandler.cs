using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;
using SKB.App.Contracts.GetWeatherForecast;
using SKB.App.Domain.GetWeatherForecast;

namespace SKB.App.Application.GetWeatherForecast;

/// <summary>
/// Query handler for the WeatherForecast
/// </summary>
public class GetWeatherForecastQueryHandler: IRequestHandler<GetWeatherForecastQuery, GetWeatherForecastQueryDto>
{
	private readonly ILogger<GetWeatherForecastQueryHandler> _logger;
	private readonly ActivitySource _activitySource;
	private readonly GetWeatherForecastController _controller;

	/// <summary>
	/// Constructor for the Handler
	/// </summary>
	/// <param name="logger">Injected Logger</param>
	/// <param name="activitySource">Injected Activity Source for Tracing</param>
	public GetWeatherForecastQueryHandler(
		ILogger<GetWeatherForecastQueryHandler> logger,
		ActivitySource activitySource
		)
	{
		this._logger = logger;
		this._activitySource = activitySource;

		// This can also be added as a Singleton to save resources
		//		Here, this dependency injection pattern is overlooked in order
		//		to keep it simple
		this._controller = new GetWeatherForecastController();
	}

	/// <summary>
	///	Handles the query to respond back with the Dtos
	/// </summary>
	/// <param name="request"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	public Task<GetWeatherForecastQueryDto> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
	{
		using var activity =
			this._activitySource
				.StartActivity($"{nameof(GetWeatherForecastQueryHandler)}.Trace");

		activity?.SetTag("Request", request);
		activity?.AddTag("Request.Method", "HttpGet");

		activity?.AddEvent(new ActivityEvent("GetWeatherForecastQueryHandler: Server Process Started"));

		this._logger.LogInformation("GetWeatherForecastQueryHandler Called");

		// Get the Weather information from the Weather Controller
		var forecast = this._controller.GetWeatherForecast();

		var response = new GetWeatherForecastQueryDto(
				TransmissionData: forecast,
				new KeyValuePair<string, object?>("Trace.RequestContext", activity?.Context)
			);
		activity?.AddEvent(new ActivityEvent("GetWeatherForecastQueryHandler: Server Process Completed"));
		return Task.FromResult(response);
	}
}
