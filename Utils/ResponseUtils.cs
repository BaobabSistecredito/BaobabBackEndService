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
        public string Item { get; set; }
        public string Message { get; set; }

        public ResponseUtils(bool status, List<T> list = null, string item = null, string message = "")
        {
            Status = status;
            List = list ?? new List<T>();
            Item = item;
            Message = message;
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
