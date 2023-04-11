using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

#nullable disable

namespace sms.Models
{
    public partial class MeasurementUnit
    {
        public MeasurementUnit()
        {
            StockItems = new HashSet<StockItem>();
        }

        public int MeasurementUnitId { get; set; }
        public string MeasurementUnitName { get; set; }
        public int SubCategoryId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual SubCategory SubCategory { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<StockItem> StockItems { get; set; }
    }
}
