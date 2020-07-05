using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppVue.Models.Json
{
    public class Image
    {
        public Int64? Id { get; set; }
        public int PreId { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}
