using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IdentyServer.Domain.Utils
{
    public class QueryResult<T>
    {
        public T[] Data { get; set; }
        public EErrorCode ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public int TotalRows { get; set; }

        public QueryResult(IEnumerable<T> data)
        {
            Data = data?.ToArray();
            TotalRows = data.Count();
        }

        public QueryResult(EErrorCode errorCode, IEnumerable<T> data)
        {
            if (data?.ToArray() != null)
            {
                Data = data?.ToArray() ?? new T[0];
                TotalRows = Data.Count();
                ErrorCode = errorCode;
            } else
            {
                Data = new T[0];
                TotalRows = 0;
                ErrorCode = errorCode;
            }
        }

        public QueryResult(EErrorCode errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public QueryResult(EErrorCode errorCode, string errorMessage, IEnumerable<T> data)
        {
            Data = data?.ToArray() ?? new T[0];
            TotalRows = Data.Count();
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }
}
