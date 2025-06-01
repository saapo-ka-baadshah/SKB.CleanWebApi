using Microsoft.Extensions.DependencyInjection;
using SKB.App.Application.Meters.RequestCounter;

namespace SKB.App.Application.Meters;

/// <summary>
/// Provides an injection extension for the meters
/// </summary>
public static class MetersInjections
{
	/// <summary>
	/// Adds all the Meters to be injected in the services
	/// </summary>
	/// <param name="services">Service Collection <see cref="IServiceCollection"/></param>
	/// <returns>Loaded IServiceCollection</returns>
	public static IServiceCollection AddMetersInjection<TAssembly>(this IServiceCollection services)
	{
		services.AddSingleton<IRequestCounter, RequestCounter<TAssembly>>();
		return services;
	}
}
