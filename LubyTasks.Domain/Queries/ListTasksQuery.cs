using Dapper;
using LubyTasks.Domain.Queries.ViewModels;
using LubyTasks.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using TaskVW = LubyTasks.Domain.Queries.ViewModels.Task;

namespace LubyTasks.Domain.Queries
{
    public class ListTasksQuery : IOperation<TaskVW>
    {

        public async Task<OperationResult<TaskVW>> ExecuteOperationAsync(LubyTasksHandler handler)
        {
            var sql = @"
                select a.id, a.title, a.description, a.concluded, a.create_date, u.id, u.name
                from tasks a inner join users u on (u.id=a.id_user)
                where a.removed=0 and u.id=@CurrentUserId
            ";

            var conn = handler.LubyTasksContext.Database.GetDbConnection();
            var result = await conn.QueryAsync<TaskVW, User, TaskVW>(sql, (action, user) =>
            {
                action.User = user;
                return action;
            },
            new { CurrentUserId = handler.CurrentUser.Id },
            splitOn: "id"
            );
            return new OperationResult<TaskVW>(HttpStatusCode.OK, result);
        }

        public async Task<OperationResult<TaskVW>> GetErrorAsync(LubyTasksHandler handler)
        {
            return await Task.FromResult<OperationResult<TaskVW>>(null);
        }
    }
}