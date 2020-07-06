using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppVue.Models.Json
{

    public class Item
    {
        public Int64? Id { get; set; }
        public int Sort { get; set; }
        public string Name { get; set; }
        public List<Int64> ImageIds { get; set; }
        public string Description { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
