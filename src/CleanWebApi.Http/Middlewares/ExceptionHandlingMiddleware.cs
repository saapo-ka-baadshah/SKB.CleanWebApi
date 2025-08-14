using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SKB.App.ChatService.Abstractions.Events;

namespace SKB.App.CleanWebApi.Http.Middlewares;

/// <summary>
/// Provides an exception handling middleware
/// </summary>
public class ExceptionHandlingMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<ExceptionHandlingMiddleware> _logger;

	/// <summary>
	/// Constructor for the error handling middleware
	/// </summary>
	/// <param name="next">Injected next request delegate.</param>
	/// <param name="logger">Injected logger.</param>
	public ExceptionHandlingMiddleware(
			RequestDelegate next,
			ILogger<ExceptionHandlingMiddleware> logger
		)
	{
		_next = next;
		_logger = logger;
	}

	/// <summary>
	/// Calls the next delegate after performing AI chat based error handling routine
	/// </summary>
	/// <param name="context">Forwarding HttpContext <see cref="HttpContext"/></param>
	public async Task InvokeAsync(HttpContext context)
	{
		IPublishEndpoint publishEndpoint = context.RequestServices.GetRequiredService<IPublishEndpoint>();
		ActivitySource activitySource = context.RequestServices.GetRequiredService<ActivitySource>();
		try
		{
			await _next(context);
		}
		catch (Exception e)
		{
			_logger.LogError("Error Captured: {error}", e.Message);
			_logger.LogInformation(
				"Calling the chat service for Error resolution, please check the logs after some time");

			await publishEndpoint.Publish(new GenericChatEvent
			{
				Prompts = [
					"There was an error processing the request. This is captured by global error handling middleware.",
					"Read through the object and provide an insightful log."
				],
				HandlingObject = e.ToString()
			});

			var problemDetails = new ProblemDetails()
			{
				Status = StatusCodes.Status500InternalServerError,
				Title = "Server Error"
			};

			context.Response.StatusCode = StatusCodes.Status500InternalServerError;
			await context.Response.WriteAsJsonAsync(problemDetails);
		}
	}
}
