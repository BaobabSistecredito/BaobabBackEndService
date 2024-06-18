using System.Collections.Generic;
using BaobabBackEndSerice.Models;

namespace BaobabBackEndService.Utils
{
    public class PaginationInfo<T>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 1;
        public int TotalRecords { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public string NextPageUrl { get; set; } = string.Empty;
    }

    public class ResponseUtils<T>
    {
        public bool IsSuccessful { get; set; }
        public List<T> Data { get; set; }
        public int StatusCode  { get; set; }
        public string Message { get; set; }
        public List<T> Errors { get; set; }
        public PaginationInfo<T> PaginationInfo { get; set; }

        public ResponseUtils(bool status, List<T> data = null, int code = 500, string message = "", List<T> errors = null, PaginationInfo<T> paginationInfo = null)
        {
            IsSuccessful = status;
            Data = data ?? new List<T>();
            StatusCode  = code;
            Message = message;
            Errors = errors ?? new List<T>();
            PaginationInfo = paginationInfo ?? new PaginationInfo<T>{};
        }
    }
}
