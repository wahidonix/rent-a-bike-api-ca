using System.Text.Json;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;
    private readonly IWebHostEnvironment _env;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger, IWebHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred.");
            await HandleExceptionAsync(context, ex, _env);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception, IWebHostEnvironment env)
    {
        var response = new ErrorResponse
        {
            Error = env.IsDevelopment() ? exception.Message : "An unexpected error occurred.",
            StackTrace = env.IsDevelopment() ? exception.StackTrace : null
        };

        var result = JsonSerializer.Serialize(response, new JsonSerializerOptions { IgnoreNullValues = true });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        return context.Response.WriteAsync(result);
    }


    public class ErrorResponse
    {
        public string Error { get; set; }
        public string StackTrace { get; set; }
    }

}
