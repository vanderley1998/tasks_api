using System.Collections.Generic;
using System.Linq;

namespace IdentyServer.Domain.Utils
{
    public class OperationResult<T>
    {
        public T[] Data { get; set; }
        public EErrorCode? ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public int? TotalRows { get; set; }

        public OperationResult(EErrorCode errorCode, IEnumerable<T> data)
        {
            Data = data?.ToArray() ?? new T[0];
            TotalRows = Data.Count();
            ErrorCode = errorCode;
        }

        public OperationResult(EErrorCode errorCode, string errorMessage)
        {
            Data = new T[0];
            TotalRows = Data.Length;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public OperationResult(EErrorCode errorCode, int totalRows)
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
