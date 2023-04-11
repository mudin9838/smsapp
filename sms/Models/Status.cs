using System;
using System.Collections.Generic;

#nullable disable

namespace sms.Models
{
    public partial class Status
    {
        public Status()
        {
            Entries = new HashSet<Entry>();
            Outs = new HashSet<Out>();
        }

        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<Entry> Entries { get; set; }
        public virtual ICollection<Out> Outs { get; set; }
    }
}
