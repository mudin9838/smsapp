using System;
using System.Collections.Generic;

#nullable disable

namespace sms.Models
{
    public partial class Entry
    {
        public int EntryId { get; set; }
        public string ParentId { get; set; }
        public string RecieptNo { get; set; }
        public string Model { get; set; }
        public string Serie { get; set; }
        public string PageNumberFrom { get; set; }
        public string PageNumberTo { get; set; }
        public int Quantity { get; set; }
        public decimal EachPrice { get; set; }
        public decimal Vat { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime EntryDate { get; set; }
        public int StockId { get; set; }
        public int StatusId { get; set; }

        public virtual Parent Parent { get; set; }
        public virtual Status Status { get; set; }
        public virtual StockItem Stock { get; set; }
    }
}
