using LubyTasks.Domain.Utils;
using System.Threading.Tasks;

namespace LubyTasks.Domain
{
    public class LubyTasksHandler
    {
        public readonly LubyTasksDbContext LubyTasksContext;
        public readonly CurrentUser CurrentUser;

        public LubyTasksHandler(LubyTasksDbContext _lubyTasksDb, CurrentUser _currentUser)
        {
            LubyTasksContext = _lubyTasksDb;
            CurrentUser = _currentUser;
        }

        public async Task<OperationResult<T>> ExecuteAsync<T>(IOperation<T> operation)
        {
            var error = await operation.GetErrorAsync(this);

            if (error == null)
                return await operation.ExecuteOperationAsync(this);

            return error;
        }
    }
}
