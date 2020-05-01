using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using System.Reflection;

namespace LubyTasks.API.Filters
{
    public class StatusRequestFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {

        }
        public void OnResultExecuting(ResultExecutingContext context)
        {
            var result = context.Result as ObjectResult;
            foreach (var prop in result.Value.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var value = prop.GetValue(result.Value, null);
                if (value != null && (value.GetType().FullName == typeof(HttpStatusCode).FullName))
                {
                    var statusCode = (HttpStatusCode)value;
                    context.HttpContext.Response.StatusCode = Convert.ToInt32(statusCode);
                }
            }
        }
    }

}
