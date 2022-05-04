using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ContactList.Filters
{
    public class ExecutionMonitorFilter : ActionFilterAttribute
    {
        private readonly Stopwatch _watch;
        private readonly ILogger<ExecutionMonitorFilter> _logger;

        public ExecutionMonitorFilter(ILogger<ExecutionMonitorFilter> logger)
        {
            _watch = new Stopwatch();
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // called before action execution
            _watch.Start();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // called after action execution
            _watch.Stop();
            // Console.WriteLine($"Executed: {context.HttpContext.Request.Path} in {_watch.ElapsedMilliseconds} ms");
            _logger.LogInformation($"Executed: {context.HttpContext.Request.Path} in {_watch.ElapsedMilliseconds} ms");
        }
    }
}
