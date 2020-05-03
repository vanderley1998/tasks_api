using LubyTasks.Domain.Utils;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace LubyTasks.API.Filters
{
    public class CurrentUserFilter : IActionFilter
    {
        private readonly CurrentUser _currentUser;

        public CurrentUserFilter(CurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            foreach(var item in context.HttpContext.Request.Headers)
            {
                if(item.Key == HttpRequestHeader.Authorization.ToString() && !string.IsNullOrEmpty(item.Value))
                {
                    _currentUser.GetTokenData(item.Value);
                    return;
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
