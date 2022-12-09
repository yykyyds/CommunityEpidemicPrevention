using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CommunityEP.Api.Utilities
{
    public class ExceptionLogFilter : IAsyncExceptionFilter
    {
        private ILogger<object> _logger;

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            //ControllerActionDescriptor? descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            //Type? type = descriptor?.MethodInfo.GetType();
            //Type type1 = descriptor.MethodInfo.DeclaringType;//获取声明此成员的类型（就是发生异常的类）

            var serviceBuilder = context.HttpContext.RequestServices;
            if(_logger == null)
                _logger = serviceBuilder.GetRequiredService<ILogger<ExceptionLogFilter>>();
            _logger.LogError($"\nStackMessage:\n{context.Exception.StackTrace}\nErrorMessage:\n{context.Exception.Message}");
            await Task.CompletedTask;
        }
    }
}
