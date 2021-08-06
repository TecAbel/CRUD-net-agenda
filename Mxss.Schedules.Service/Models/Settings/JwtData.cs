using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mxss.Schedules.Service.Models.Settings
{
    public class JwtData
    {
        public string Key { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Expiration { get; set; }
    }
}
