using SKB.App.Domain.GetWeatherForecast;

namespace SKB.App.Contracts.GetWeatherForecast;

/// <summary>
/// Singular Entropy Dto for GetWeatherForecastQuery
/// </summary>
/// <param name="TransmissionData"></param>
/// <param name="Metadata"></param>
public record GetWeatherForecastQueryDto(
		GetWeatherForecastTransmissionData[] TransmissionData,
		params KeyValuePair<string, object?>[]? Metadata
	);
