using System;
using System.Collections.Generic;

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

        public int StockId { get; set; }
        public string ParentId { get; set; }
        public string Serie { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int MeasurementUnitId { get; set; }
        public int Quantity { get; set; }
        public decimal EachPrice { get; set; }
        public decimal Vat { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime RegisteredDate { get; set; }

        public virtual Category Category { get; set; }
        public virtual MeasurementUnit MeasurementUnit { get; set; }
        public virtual Parent Parent { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual ICollection<Entry> Entries { get; set; }
        public virtual ICollection<Out> Outs { get; set; }
    }
}
