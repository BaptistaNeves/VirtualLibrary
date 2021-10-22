using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VirtualLibrary.Api.Errors;

namespace VirtualLibrary.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next,
                                     ILogger<ExceptionMiddleware> logger,
                                     IHostEnvironment env)
        {
            //RequestDelegate is what's next, what is coming up next in the middleware pipeline
            //We need ILogger to display our exception in the terminal
            //IHostEnvironment to check what environment we're running
            
            _next = next;
            _logger = logger;
            _env = env;
        }

 //This is a required method for a Middleware
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                 //To log the exception in the terminal
                _logger.LogError(ex.Message);

                 //Set the Exception in the HttpContextResponse
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                //Create the response
                var response = _env.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    : new ApiException(context.Response.StatusCode, "Erro Interno do Servidor");

                 //Serialize the response to json camelcase
                 var options = new JsonSerializerOptions 
                 {
                     PropertyNamingPolicy = JsonNamingPolicy.CamelCase                                                            
                };

                 var json = JsonSerializer.Serialize(response,options);

                 //Write the Exception to the the HttpContextResponse
                 await context.Response.WriteAsync(json);
            }
        }

    }
}