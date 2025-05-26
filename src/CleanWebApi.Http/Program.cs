using JetBrains.Annotations;
using SKB.App.Application;
using SKB.App.CleanWebApi.Http.Extensions;
using SKB.Core.Hosting.Extensions.Configurations;
using SKB.Core.Hosting.Extensions.Instrumentations;
using SKB.Core.Hosting.Extensions.OpenTelemetry;
using SKB.Core.Hosting.Extensions.WebApi;

namespace SKB.App.CleanWebApi.Http;

[PublicAPI]
internal class Program
{
    private static void Main(string[] args)
    {
	    // Create Web API Application Builder
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Standard Configurations
        builder.AddConfigurations();
		builder.AddOpenTelemetry();
		builder.AddOpenTelemetryApiExtensions<Program>();
		builder.Services.AddInstrumentation<Program>();

		// Standard Service Configurations
		builder.Services.AddMediatR(
			cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly)
			);
		builder.Services.AddMvcCore().AddApiExplorer();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(o =>
		{
			o.SwaggerDoc("v1", new() { Title = "CleanWebApi", Version = "v1" });
		});

		builder.Services.AddApplication();

		// Create the application
		WebApplication app = builder.Build();

		// Use the swagger, only if the environment is not production
		if (!builder.Environment.IsProduction())
		{
			app.UseSwagger();
			app.UseSwaggerUI(o =>
			{
				o.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
			});
		}

		app.UseRouting();

		app.UseHttpsRedirection();

		app.AddEndpoints<Program>();

		// Run the application
		app.Run();
    }
}
