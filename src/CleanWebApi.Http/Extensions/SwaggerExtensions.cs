using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SKB.Core.Hosting.Extensions.Auth;

namespace SKB.App.CleanWebApi.Http.Extensions;

/// <summary>
/// Adds the extensions for the swagger calls
/// </summary>
public static class SwaggerExtensions
{
	/// <summary>
	/// Allows adding the swagger with auth layer
	/// </summary>
	/// <param name="services">Builder service collection.</param>
	/// <param name="configuration">Provided builder configurations.</param>
	/// <returns>Service loaded collection.</returns>
	public static IServiceCollection AddSwaggerGenWithAuth(
		this IServiceCollection services,
		IConfiguration configuration
		)
	{
		var options = configuration.GetSection("Keycloak").Get<KeycloakOptions>();
		services.AddSwaggerGen(o =>
		{
			o.SwaggerDoc("v1", new() { Title = "CleanWebApi", Version = "v1" });
			if (options is not null)
			{
				// Adds a custom id strategy to trace the objects correctly
				o.CustomSchemaIds(id => id.FullName!.Replace("+", "-"));
				o.AddSecurityDefinition("Keycloak", new OpenApiSecurityScheme
				{
					Type = SecuritySchemeType.OAuth2,
					Flows = new OpenApiOAuthFlows
					{
						Implicit = new OpenApiOAuthFlow
						{
							AuthorizationUrl = new Uri(options.AuthorizationUrl!),
							Scopes = new Dictionary<string, string>
							{
								{"openid", "openid"},
								{"profile", "profile"},
							}
						}
					}
				});

				var securityRequirements = new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Id = "Keycloak",
								Type = ReferenceType.SecurityScheme
							},
							In = ParameterLocation.Header,
							Name = "Bearer",
							Scheme = "Bearer"
						},
						[]
					}
				};

				o.AddSecurityRequirement(securityRequirements);
			}
		});
		return services;
	}
}
