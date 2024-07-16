using System.Net;
using Newtonsoft.Json;

namespace PriceList;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            const HttpStatusCode code = HttpStatusCode.InternalServerError;

            var result = JsonConvert.SerializeObject(new
            {
                message = exception.Message,
                detail = exception.ToString()
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            await context.Response.WriteAsync(result);
        }
    }
}