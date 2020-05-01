using LubyTasks.Domain.Utils;
using System.Threading.Tasks;

namespace LubyTasks.Domain
{
    public class LubyTasksHandler
    {
        public readonly LubyTasksDbContext LubyTasksContext;

        public LubyTasksHandler(LubyTasksDbContext LubyTasksDb)
        {
            LubyTasksContext = LubyTasksDb;
        }

        public async Task<OperationResult<T>> ExecuteAsync<T>(IOperation<T> operation)
        {
            var error = await operation.GetError(this);

            if (error == null)
                return await operation.ExecuteOperationAsync(this);

            return error;
        }
    }
}
