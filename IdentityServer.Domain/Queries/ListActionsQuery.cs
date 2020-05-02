using Dapper;
using LubyTasks.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading.Tasks;
namespace LubyTasks.Domain.Queries
{
    public class ListActionsQuery : IOperation<ViewModels.Action>
    {
        public async Task<OperationResult<ViewModels.Action>> ExecuteOperationAsync(LubyTasksHandler handler)
        {
            var sql = @"
                select id, description, concluded, id_user, create_date, last_modified
                from actions a where a.removed=0
            ";

            var conn = handler.LubyTasksContext.Database.GetDbConnection();
            var result = await conn.QueryAsync<ViewModels.Action>(sql);
            return new OperationResult<ViewModels.Action>(HttpStatusCode.OK, result);
        }

        public async Task<OperationResult<ViewModels.Action>> GetErrorAsync(LubyTasksHandler handler)
        {
            return await Task.FromResult<OperationResult<ViewModels.Action>>(null);
        }
    }
}
