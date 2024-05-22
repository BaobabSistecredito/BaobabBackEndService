using System.Collections.Generic;
using BaobabBackEndSerice.Models;

namespace BaobabBackEndService.Utils
{
    public class ResponseUtils<T>
    {
        private List<Category> list;
        private object item;

        public bool Status { get; set; }
        public List<T> List { get; set; }
        public object Item { get; set; } // Usando object en lugar de T
        public string Message { get; set; }
        public List<T> Errors { get; set; }

        public ResponseUtils(bool status, List<T> list = null, object item = null, string message = "", List<T> errors = null)
        {
            Status = status;
            List = list ?? new List<T>();
            Item = item;
            Message = message;
            Errors = errors ?? new List<T>();
        }

        public ResponseUtils(bool status, List<Category> list, object item, string message)
        {
            Status = status;
            this.list = list;
            this.item = item;
            Message = message;
        }
    }
}
