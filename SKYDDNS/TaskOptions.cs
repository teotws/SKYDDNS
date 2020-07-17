using SKYDDNS.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKYDDNS
{
    public class TaskOptions
    {
        public int Interval { get; set; } = 10000;

        public string ZoneId { get; set; }

        public string Type { get; set; } = DNSType.A;

        public string Names { get; set; }




    }
}
