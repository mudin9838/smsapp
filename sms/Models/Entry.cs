using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
#nullable disable

namespace sms.Models
{
    public partial class Entry
    {
        public int EntryId { get; set; }
        public string ParentId { get; set; }
        public string RecieptNo { get; set; }
        public string Serie { get; set; }
        public string PageNumberFrom { get; set; }
        public string PageNumberTo { get; set; }
        public int Quantity { get; set; }
        public decimal EachPrice { get; set; }
        public decimal Vat { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime EntryDate { get; set; }
        public int StockId { get; set; }
        public int StatusId { get; set; } = 1;
    public int StatusdelId { get; set; } = 1;



        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Parent Parent { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Status Status { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual StockItem Stock { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Statusdel Statusdel { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ApplicationUser User { get; set; }

    }
}
