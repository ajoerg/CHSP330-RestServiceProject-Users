using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UsersRestService
{
    public class LoggingActionFilter : IActionFilter
    {
        private System.Diagnostics.Stopwatch? stopwatch;

        private readonly IWebHostEnvironment env;

        public LoggingActionFilter(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public void OnActionExecuting(ActionExecutingContext actionContext)
        {
            stopwatch = System.Diagnostics.Stopwatch.StartNew();
        }

        public void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            stopwatch.Stop();

            var controllerName = actionExecutedContext.Controller.ToString();
            var actionName = ((ControllerBase)actionExecutedContext.Controller).Request.Method;

            var webroot = env.ContentRootPath;
            var filepath = Path.Combine(webroot, "logger.txt");
            var logline = string.Format("{0} : {1}, {2}, Elapsed={3}\n",
                System.DateTime.Now, controllerName, actionName, stopwatch.Elapsed);

            File.AppendAllText(filepath, logline);
        }
    }
}
