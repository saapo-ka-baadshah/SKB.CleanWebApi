using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SKB.App.ChatService.Core.Extensions.MassTransit;
using SKB.App.Infrastructure.Messaging.Extensions;

namespace SKB.App.Infrastructure.Messaging;

/// <summary>
/// Adds messaging infrastructure
/// </summary>
public static class MessagingInfraExtensions
{
	/// <summary>
	/// Adds the messaging infrastructure to the service collection
	/// </summary>
	/// <returns>infrastructure loaded service collection</returns>
	public static IServiceCollection AddMessagingInfrastructure(
			this IServiceCollection services,
			IConfiguration configuration
		)
	{
		services.AddMassTransit(cfg =>
		{
			cfg.AddChatConsumer();
			cfg.AddRabbitMq(configuration);
		});
		return services;
	}
}
