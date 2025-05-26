using MediatR;

namespace SKB.App.Contracts.GetWeatherForecast;

/// <summary>
/// Query for the Weather retrieval
/// </summary>
public class GetWeatherForecastQuery: IRequest<GetWeatherForecastQueryDto>
{

}
