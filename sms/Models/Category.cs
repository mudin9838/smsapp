using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

#nullable disable

namespace sms.Models
{
    public partial class Category
    {
        public Category()
        {
            StockItems = new HashSet<StockItem>();
            SubCategories = new HashSet<SubCategory>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]

        public virtual ICollection<StockItem> StockItems { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
