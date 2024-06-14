using System.Collections.Generic;
using BaobabBackEndSerice.Models;

namespace BaobabBackEndService.Utils
{
    public class ResponseUtils<T>
    {
        public bool Status { get; set; }
        public List<T> List { get; set; }
        public object Code { get; set; }
        public string Message { get; set; }
        public List<T> Errors { get; set; }

        public ResponseUtils(bool status, List<T> list = null, object code = null, string message = "", List<T> errors = null)
        {
            Status = status;
            List = list ?? new List<T>();
            Code = code;
            Message = message;
            Errors = errors ?? new List<T>();
        }
    }
}
