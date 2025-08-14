using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SKB.App.Contracts.GetWeatherForecast;
using SKB.Core.Abstractions.WebApi;

namespace SKB.App.CleanWebApi.Http.Endpoints;

/// <summary>
/// CleanWebApi's Sample Endpoint to demonstrate the Functionalities
/// </summary>
public class CleanWebApiSampleEndpoint: IBaseEndpoint
{
	/// <inheritdoc/>
	public void RegisterAllMethods(IEndpointRouteBuilder app)
	{
		app.MapGet("/weatherforecast", async (
				IMediator mediator
				) =>
			{
				var req = new GetWeatherForecastQuery();
				var responseDto = await mediator.Send(req);
				return responseDto;
			})
			.RequireAuthorization(new AuthorizeAttribute
			{
				Roles = "user"
			})
			.WithName("GetWeatherForecast")
			.WithSummary("This is a sample API Endpoint")
			.WithDescription("This is not supposed to part of your Production Stack");
	}
}
