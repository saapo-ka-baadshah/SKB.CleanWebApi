using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SKB.App.Application.GetWeatherForecast;
using SKB.App.Contracts.GetWeatherForecast;

namespace SKB.App.Application;

/// <summary>
/// Application layer dependency injections
/// </summary>
public static class ApplicationDependencyInjection
{
	/// <summary>
	/// Adds Application Layer Dependency Injections
	/// </summary>
	/// <param name="services"></param>
	/// <returns></returns>
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		// Add all your CQRS request handlers
		services.AddTransient<IRequestHandler<GetWeatherForecastQuery, GetWeatherForecastQueryDto>, GetWeatherForecastQueryHandler>();
		return services;
	}
}
