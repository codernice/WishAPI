using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GS.Wish.Model
{
    public class SyncWishOrdersFilter
    {
        public int start { get; set; }
        public int limit { get; set; }
        public string since { get;set; }
    }
}
