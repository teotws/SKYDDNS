using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKYDDNS.Cloudflare.Dtos
{
    public class DNSRecordInput
    {
        public string type { get; set; }

        public string name { get; set; }

        public string content { get; set; }

        public int ttl { get; set; } = 120;

        public int priority { get; set; } = 0;

        public bool proxied { get; set; } = false;
    }
}
