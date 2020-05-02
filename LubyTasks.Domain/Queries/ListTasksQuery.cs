using Dapper;
using LubyTasks.Domain.Queries.ViewModels;
using LubyTasks.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading.Tasks;
namespace LubyTasks.Domain.Queries
{
    public class ListTasksQuery : IOperation<ViewModels.Task>
    {

        public async Task<OperationResult<ViewModels.Task>> ExecuteOperationAsync(LubyTasksHandler handler)
        {
            var sql = @"
                select a.id, a.title, a.description, a.concluded, a.create_date, u.id, u.name
                from tasks a inner join users u on (u.id=a.id_user)
                where a.removed=0 and u.id=@CurrentUserId
            ";

            var conn = handler.LubyTasksContext.Database.GetDbConnection();
            var result = await conn.QueryAsync<ViewModels.Task, User, ViewModels.Task>(sql, (action, user) =>
            {
                action.User = user;
                return action;
            },
            new { CurrentUserId = handler.CurrentUser.Id },
            splitOn: "id"
            );
            return new OperationResult<ViewModels.Task>(HttpStatusCode.OK, result);
        }

        public async Task<OperationResult<ViewModels.Task>> GetErrorAsync(LubyTasksHandler handler)
        {
            return await System.Threading.Tasks.Task.FromResult<OperationResult<ViewModels.Task>>(null);
        }
    }
}
