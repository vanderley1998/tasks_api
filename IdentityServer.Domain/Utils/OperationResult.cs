using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace IdentyServer.Domain.Utils
{
    public class OperationResult<T>
    {
        public T[] Data { get; set; }
        public HttpStatusCode? ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public int? TotalRows { get; set; }

        public OperationResult(HttpStatusCode errorCode, IEnumerable<T> data)
        {
            Data = data?.ToArray() ?? new T[0];
            TotalRows = Data.Count();
            ErrorCode = errorCode;
        }

        public OperationResult(HttpStatusCode errorCode, string errorMessage)
        {
            Data = new T[0];
            TotalRows = Data.Length;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public OperationResult(HttpStatusCode errorCode, int totalRows)
        {
            Data = new T[0];
            TotalRows = totalRows;
            ErrorCode = errorCode;
        }

        public object GetResult(object data)
        {
            return new
            {
                TotalRows,
                data,
                ErrorCode,
                ErrorMessage
            };
        }
    }
}
