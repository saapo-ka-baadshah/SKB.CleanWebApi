using System.Reflection;
using OpenTelemetry.Resources;

namespace SKB.App.CleanWebApi.Http.Extensions;

/// <summary>
/// Add the additional info set for OpenTelemetry
/// </summary>
public static class OpenTelemetryApiExtensions
{
	/// <summary>
	/// Adds additional set of OpenTelemetry Options to the API
	/// </summary>
	/// <param name="builder"></param>
	/// <returns></returns>
	public static IHostApplicationBuilder AddOpenTelemetryApiExtensions<TAssembly>(this IHostApplicationBuilder builder)
	{
		builder.Services.AddOpenTelemetry()
			.ConfigureResource(configure =>
			{
				configure.AddService(typeof(TAssembly).Namespace!, System.Environment.MachineName);
			})
			.WithMetrics(configure =>
			{
				configure.AddMeter($"{typeof(TAssembly).Namespace}.Meter");
			})
			.WithTracing(configure =>
			{
				configure.AddSource($"{typeof(TAssembly).Namespace}.Activity");
			});
		return builder;
	}
}
