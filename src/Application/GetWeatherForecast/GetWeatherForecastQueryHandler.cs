using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;
using SKB.App.Application.Meters.RequestCounter;
using SKB.App.Contracts.GetWeatherForecast;

namespace SKB.App.Application.GetWeatherForecast;

/// <summary>
/// Query handler for the WeatherForecast
/// </summary>
public class GetWeatherForecastQueryHandler: IRequestHandler<GetWeatherForecastQuery, GetWeatherForecastQueryDto>
{
	private readonly ILogger<GetWeatherForecastQueryHandler> _logger;
	private readonly ActivitySource _activitySource;
	private readonly IRequestCounter _requestCounter;

	private readonly string[] _summaries = new[]
	{
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};

	/// <summary>
	/// Constructor for the Handler
	/// </summary>
	/// <param name="logger">Injected Logger</param>
	/// <param name="activitySource">Injected Activity Source for Tracing</param>
	/// <param name="requestCounter">Injected Request Counter</param>
	public GetWeatherForecastQueryHandler(
		ILogger<GetWeatherForecastQueryHandler> logger,
		ActivitySource activitySource,
		IRequestCounter requestCounter
		)
	{
		this._logger = logger;
		this._activitySource = activitySource;
		this._requestCounter = requestCounter;
	}

	/// <summary>
	///	Handles the query to respond back with the Dtos
	/// </summary>
	/// <param name="request"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	public Task<GetWeatherForecastQueryDto> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
	{
		this._requestCounter.Add(1);
		using var activity =
			this._activitySource
				.StartActivity($"{nameof(GetWeatherForecastQueryHandler)}.Trace");

		activity?.SetTag("Request", request);
		activity?.AddTag("Request.Method", "HttpGet");

		activity?.AddEvent(new ActivityEvent("GetWeatherForecastQueryHandler: Server Process Started"));

		this._logger.LogInformation("GetWeatherForecastQueryHandler Called");
		var forecast = Enumerable.Range(1, 5).Select(index =>
				new GetWeatherForecastTransmissionData
				(
					DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
					Random.Shared.Next(-20, 55),
					_summaries[Random.Shared.Next(_summaries.Length)]
				))
			.ToArray();
		var response = new GetWeatherForecastQueryDto(
				TransmissionData: forecast,
				new KeyValuePair<string, object?>("Trace.RequestContext", activity?.Context)
			);
		activity?.AddEvent(new ActivityEvent("GetWeatherForecastQueryHandler: Server Process Completed"));
		return Task.FromResult(response);
	}
}
