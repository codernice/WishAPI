using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Wish.Model
{
    public class RefreshTokenReturn
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public int expires_in { get; set; }
        public long expiry_time { get; set; }
        public string expiry_string { get; set; }
    }
}
