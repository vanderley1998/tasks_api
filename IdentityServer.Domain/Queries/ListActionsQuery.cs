using Dapper;
using LubyTasks.Domain.Queries.ViewModels;
using LubyTasks.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading.Tasks;
namespace LubyTasks.Domain.Queries
{
    public class ListActionsQuery : IOperation<Action>
    {

        public async Task<OperationResult<Action>> ExecuteOperationAsync(LubyTasksHandler handler)
        {
            var sql = @"
                select a.id, a.title, a.description, a.concluded, a.create_date, u.id, u.name
                from actions a inner join users u on (u.id=a.id_user)
                where a.removed=0 and u.id=@CurrentUserId
            ";

            var conn = handler.LubyTasksContext.Database.GetDbConnection();
            var result = await conn.QueryAsync<Action, User, Action>(sql, (action, user) =>
            {
                action.User = user;
                return action;
            },
            new { CurrentUserId = handler.CurrentUser.Id },
            splitOn: "id"
            );
            return new OperationResult<Action>(HttpStatusCode.OK, result);
        }

        public async Task<OperationResult<Action>> GetErrorAsync(LubyTasksHandler handler)
        {
            return await Task.FromResult<OperationResult<Action>>(null);
        }
    }
}
