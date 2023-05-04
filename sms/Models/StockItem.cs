using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

#nullable disable

namespace sms.Models
{
    public partial class StockItem
    {
        public StockItem()
        {
            Entries = new HashSet<Entry>();
            Outs = new HashSet<Out>();
        }
        [Key]
        public int StockId { get; set; }
        public string ParentId { get; set; }
        public string Serie { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }

        public string Model { get; set; }
        public int MeasurementUnitId { get; set; }
        public int Quantity { get; set; }
        public decimal EachPrice { get; set; }
        public decimal Vat { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime RegisteredDate { get; set; }
        public int StatusdelId { get; set; } = 1;
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Category Category { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual MeasurementUnit MeasurementUnit { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Parent Parent { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual SubCategory SubCategory { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Statusdel Statusdel{ get; set; }

        [JsonIgnore]
        [IgnoreDataMember]

        public virtual ICollection<Entry> Entries { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Out> Outs { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ApplicationUser User { get; set; }
    }
}
