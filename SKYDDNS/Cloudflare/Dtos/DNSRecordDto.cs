using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKYDDNS.Cloudflare.Dtos
{
    public class DNSRecordDto
    {
        public string id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string content { get; set; }
        public bool proxiable { get; set; }
        public bool proxied { get; set; }
        public int ttl { get; set; }
        public bool locked { get; set; }
        public string zone_id { get; set; }
        public string zone_name { get; set; }
        public DateTime? created_on { get; set; }
        public DateTime? modified_on { get; set; }

    }
}
