using ApplicationCore.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieShopMVC.Infrastruce
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MovieShopExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MovieShopExceptionMiddleware> _logger;
        public MovieShopExceptionMiddleware(RequestDelegate next, ILogger<MovieShopExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            _logger.LogInformation("Exception Middleware Begining");

            try
            {
                // when everrthing is good,
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // when any exception happnes then handle here
                // MovieSerive => exceptiom
                _logger.LogError($"Catching the exception in Middleware{ex}");

                await HandleException(httpContext, ex);
            }



        }
        private async Task HandleException(HttpContext httpContext, Exception ex)
        {
            // catch the actual type of exception, set the http status code,
            // log the deta
            // URL, COntroller/Action, DateTime, StackTrace, Error Message, USerId(if user is authenticated), Ip Address using SeriLog
            // Send email using MailKit to Dev Team
            // display a friendly page to User
            // 200 OK, 201, Created, 400 Bad request, 401 UnAuthorized, 403 Forbidden, 404 NotFound, 500 Internal Server Error
            switch (ex)
            {
                case ConflictException _:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case NotFoundException _:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                default:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }


            // URL, Controller/Action, DateTime, StackTrace, Error Message, USerId(if user is authenticated), Ip Address using SeriLog
            // Log the excetpions using SeriLog Text or JSON files...

            //Log.ForContext().Information("111");
            Log.Logger = new LoggerConfiguration().MinimumLevel.Information() // <- Set the minimum level
                .WriteTo.File("Log\\log.txt", rollingInterval: RollingInterval.Day).CreateLogger();

            var userId = httpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Log.Error("Url: {Host}", httpContext.Request.Host);
            Log.Error("Status code :{StatusCode}", httpContext.Response.StatusCode);
            Log.Error(ex, "StackTrace : {StackTrace}", ex.StackTrace);
            Log.Error(ex, "Error Message is {Message}", ex.Message);
            Log.Error("UserId: {userId}", userId);

            //Log.Information("status code", httpContext.Response.StatusCode);
            //var log = new LoggerConfiguration().Enrich.FromLogContext()
            //    .WriteTo.File("log.txt").CreateLogger()
            //    ;

            //using (LogContext.PushProperty("URL", httpContext.Response.StatusCode))
            //{
            //    Log.Information("URL:",
            //        httpContext.Response.StatusCode);
            //}
            //var log = new LoggerConfiguration().WriteTo.File("log.txt", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error,
            //        rollingInterval: RollingInterval.Day, outputTemplate:"{ Timestamp: yyyy - MM - dd HH: mm: ss.fff zzz} [{ Level: u3}] { Message: j} {NewLine}{Exception}");
            //Log.logger = new LoggerConfiguration()

            // Send email using MailKit to Dev Team
            httpContext.Response.Redirect("/Home/Error");

            await Task.CompletedTask;
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MovieShopExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseMovieShopExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MovieShopExceptionMiddleware>();
        }
    }
}
