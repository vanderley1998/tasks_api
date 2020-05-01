using System.Threading.Tasks;

namespace LubyTasks.Domain.Utils
{
    public interface IOperation<T>
    {
        public Task<OperationResult<T>> GetError(LubyTasksHandler handler);
        public Task<OperationResult<T>> ExecuteOperationAsync(LubyTasksHandler handler);
    }
}