using Dapper;
using LubyTasks.Domain;
using LubyTasks.Domain.Queries.ViewModels;
using LubyTasks.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LubyTasks.LubyTasks.Queries
{
    public class GetAuthQuery : IOperation<CredentialUser>
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public async Task<OperationResult<CredentialUser>> ExecuteOperationAsync(LubyTasksHandler handler)
        {
            var sql = @"
                select u.id, u.login, u.password
                from users u
                where u.login=@Login and u.password=@Password
                and u.removed=0
            ";

            var conn = handler.LubyTasksContext.Database.GetDbConnection();
            var result = await conn.QueryAsync<CredentialUser>(sql, new { Login, Password });
            if(result.Count() == 0)
                return new OperationResult<CredentialUser>(HttpStatusCode.Unauthorized, result);

            return new OperationResult<CredentialUser>(HttpStatusCode.OK, result);
        }

        public async Task<OperationResult<CredentialUser>> GetErrorAsync(LubyTasksHandler handler)
        {
            if (string.IsNullOrWhiteSpace(Login))
                return new OperationResult<CredentialUser>(HttpStatusCode.BadRequest, $"Parameter {nameof(Login) } is null or empty");

            if (string.IsNullOrWhiteSpace(Password))
                return new OperationResult<CredentialUser>(HttpStatusCode.BadRequest, $"Parameter {nameof(Password) } is null or empty");

            return await System.Threading.Tasks.Task.FromResult<OperationResult<CredentialUser>>(null);
        }
    }
}
