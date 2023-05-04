using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

#nullable disable

namespace sms.Models
{
    public partial class Out
    {
        public int OutId { get; set; }
        public int RecieptNo { get; set; }
        public string ParentId { get; set; }
        public int DepartmentId { get; set; }
        public int EmployeeId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Serie { get; set; }
        public int Quantity { get; set; }
        public decimal EachPrice { get; set; }
        public decimal Vat { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OutDate { get; set; }
        public int StockId { get; set; }

        public int StatusId { get; set; } = 1;
        public int StatusdelId { get; set; } = 1;

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Department Department { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Employee Employee { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Parent Parent { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Status Status { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Statusdel Statusdel { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual StockItem Stock { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ApplicationUser User { get; set; }

    }
}
