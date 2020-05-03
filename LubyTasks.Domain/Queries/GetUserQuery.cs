using Dapper;
using LubyTasks.Domain.Queries.ViewModels;
using LubyTasks.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace LubyTasks.Domain.Queries
{
    public class GetUserQuery : IOperation<User>
    {
        public async Task<OperationResult<User>> ExecuteOperationAsync(LubyTasksHandler handler)
        {
            var sql = @"
                select u.id, u.name, u.login
                from users u
                where u.id=@CurrentUserId
            ";

            var conn = handler.LubyTasksContext.Database.GetDbConnection();
            var result = await conn.QueryAsync<User>(sql, new { CurrentUserId = handler.CurrentUser.Id });
            return new OperationResult<User>(HttpStatusCode.OK, result);
        }

        public async Task<OperationResult<User>> GetErrorAsync(LubyTasksHandler handler)
        {
            if (handler.CurrentUser.Id == 0)
                return new OperationResult<User>(HttpStatusCode.Unauthorized, $"There's no opened session. Please, get the token and try again");

            return await Task.FromResult<OperationResult<User>>(null);
        }
    }
}
