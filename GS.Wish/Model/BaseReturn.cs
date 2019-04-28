using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GS.Wish.Model
{
    public class BaseReturn<T>
    {
        public string message { get; set; }
        public int code { get; set; }
        public T data { get; set; }
        public BasePagingReturn paging { get; set; }
    }

    public class NormalReturn
    {
        public string message { get; set; }
        public int code { get; set; }
    }

    public class BasePagingReturn
    {
        public string next { get; set; }
        public string previous { get; set; }
    }
}
