using Microsoft.AspNetCore.Http;
using aspnetcore.Domain;
using aspnetcore.Domain.Resource;

namespace aspnetcore
{
    // hứng exception
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }


        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Console.WriteLine(exception);
            context.Response.ContentType = "application/json";

            switch (exception)
            {
                case DataInvalidException dataInvalidException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync(text: new BaseException()
                    {
                        StatusCode = dataInvalidException.StatusCode,
                        UserMessage = ResourceVN.Error_NotFound,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? "");
                    break;

                case NotFoundException notFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsync(text: new BaseException()
                    {
                        StatusCode = notFoundException.StatusCode,
                        UserMessage = ResourceVN.Error_NotFound,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? "");
                    break;

                case ConflictException conflictException:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    await context.Response.WriteAsync(text: new BaseException()
                    {
                        StatusCode = conflictException.StatusCode,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        UserMessage = exception.Message,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? "");
                    break;

                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync(text: new BaseException()
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        DevMessage = exception.Message,
                        UserMessage = ResourceVN.Error_Exception,
                        MoreInfo = exception.HelpLink,
                        TraceId = context.TraceIdentifier
                    }.ToString() ?? "");
                    break;
            }
        }
    }
}
