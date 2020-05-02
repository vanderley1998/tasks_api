using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace LubyTasks.Domain.Utils
{
    public class OperationResult<T>
    {
        public T[] Data { get; set; }
        public HttpStatusCode? StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public int? TotalRows { get; set; }

        public OperationResult(HttpStatusCode statusCode, IEnumerable<T> data)
        {
            Data = data?.ToArray();
            TotalRows = Data?.Count();
            StatusCode = statusCode;
        }

        public OperationResult(HttpStatusCode statusCode, string errorMessage)
        {
            TotalRows = Data?.Length;
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }

        public OperationResult(HttpStatusCode statusCode, int totalRows)
        {
            TotalRows = totalRows;
            StatusCode = statusCode;
        }

        public object GetResult(object data)
        {
            return new
            {
                TotalRows,
                data,
                StatusCode,
                ErrorMessage
            };
        }
    }
}
