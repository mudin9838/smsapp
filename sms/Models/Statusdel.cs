using System;
using System.Collections.Generic;

#nullable disable

namespace sms.Models
{
    public partial class Statusdel
    {
        public Statusdel()
        {
            Entries = new HashSet<Entry>();
            Outs = new HashSet<Out>();
            StockItems = new HashSet<StockItem>();
        }

        public int StatusdelId { get; set; }
        public string StatusdelName { get; set; }

        public virtual ICollection<Entry> Entries { get; set; }
        public virtual ICollection<Out> Outs { get; set; }
        public virtual ICollection<StockItem> StockItems { get; set; }
    }
}
