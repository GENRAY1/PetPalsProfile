﻿using Microsoft.AspNetCore.Mvc;

namespace PetPalsProfile.Api.Middleware;

/// <summary>
/// Exception handling middleware
/// (Обработка исключений)
/// </summary>
internal sealed class ExceptionHandlingMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<ExceptionHandlingMiddleware> _logger;

	public ExceptionHandlingMiddleware(
		RequestDelegate next,
		ILogger<ExceptionHandlingMiddleware> logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception exception)
		{
			_logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

			ExceptionDetails exceptionDetails = GetExceptionDetails(exception);

			var problemDetails = new ProblemDetails
			{
				Status = exceptionDetails.Status,
				Type = exceptionDetails.Type,
				Title = exceptionDetails.Title,
				Detail = exceptionDetails.Detail,
			};

			if (exceptionDetails.Errors is not null)
			{
				problemDetails.Extensions["errors"] = exceptionDetails.Errors;
			}

			context.Response.StatusCode = exceptionDetails.Status;

			await context.Response.WriteAsJsonAsync(problemDetails);
		}
	}

	private static ExceptionDetails GetExceptionDetails(Exception exception)
	{
		return exception switch
		{
			_ => new ExceptionDetails(
				StatusCodes.Status500InternalServerError,
				"ServerError",
				"Server error",
				exception.Message,
				null)
		};
	}

	internal sealed record ExceptionDetails(
		int Status,
		string Type,
		string Title,
		string Detail,
		IEnumerable<object>? Errors);
}
