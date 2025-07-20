using MassTransit;
using Microsoft.Extensions.Configuration;

namespace SKB.App.Infrastructure.Messaging.Extensions;

/// <summary>
/// Adds RabbitMq messaging infrastructure to the core messaging infrastructure stack
/// </summary>
public static class RabbitMqExtensions
{
	/// <summary>
	/// Adds RabbitMq configurations to the bus registrations
	/// </summary>
	/// <param name="configurator">bus registration configurator</param>
	/// <param name="configuration">configuration for the application</param>
	/// <returns>Bus configurator loaded with RabbitMq</returns>
	public static IBusRegistrationConfigurator AddRabbitMq(
		this IBusRegistrationConfigurator configurator,
		IConfiguration configuration)
	{
		RabbitMqTransportOptions? transportOptions = configuration
			.GetSection("RabbitMq")
			.Get<RabbitMqTransportOptions>();
		if (transportOptions == null)
		{
			return configurator;
		}
		configurator.UsingRabbitMq((context, cfg) =>
		{
			cfg.Host(
					transportOptions.Host,
					transportOptions.Port,
					"/",
					host =>
					{
						host.Username(transportOptions.User);
						host.Password(transportOptions.Pass);
					}
				);
			cfg.ConfigureEndpoints(context);
		});
		return configurator;
	}
}
