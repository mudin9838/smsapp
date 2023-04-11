using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

#nullable disable

namespace sms.Models
{
    public partial class Parent
    {
        public Parent()
        {
            Entries = new HashSet<Entry>();
            Outs = new HashSet<Out>();
            StockItems = new HashSet<StockItem>();
        }

        public string ParentId { get; set; }
        public string ParentName { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Entry> Entries { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Out> Outs { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]

        public virtual ICollection<StockItem> StockItems { get; set; }
    }
}
