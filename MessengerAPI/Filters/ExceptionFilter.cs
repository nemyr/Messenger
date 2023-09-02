using Microsoft.AspNetCore.Mvc.Filters;

namespace MessengerAPI.Filters
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            //todo: write errors to db
            if (context == null)
                return Task.CompletedTask;
            Console.WriteLine(context.Exception.Message);
            context.HttpContext.Response.StatusCode = 500;
            context.HttpContext.Response.WriteAsync(context.Exception.Message);
            return Task.CompletedTask;
        }
    }
}
