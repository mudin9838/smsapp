using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

#nullable disable

namespace sms.Models
{
    public partial class SubCategory
    {
        public SubCategory()
        {
            MeasurementUnits = new HashSet<MeasurementUnit>();
            StockItems = new HashSet<StockItem>();
        }

        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Category Category { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<MeasurementUnit> MeasurementUnits { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]

        public virtual ICollection<StockItem> StockItems { get; set; }
    }
}
