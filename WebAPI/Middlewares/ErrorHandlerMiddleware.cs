using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Authentication;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebAPI.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                var statusCode = error switch
                {
                    AuthenticationException => HttpStatusCode.Unauthorized, 
                    InvalidOperationException => HttpStatusCode.BadRequest,
                    KeyNotFoundException => HttpStatusCode.NotFound,
                    ArgumentOutOfRangeException => HttpStatusCode.BadRequest,
                    _ => HttpStatusCode.InternalServerError
                };

                response.StatusCode = (int) statusCode;

                var result = JsonSerializer.Serialize(new {error.Message});
                await response.WriteAsync(result); 
            }
        }
    }
}