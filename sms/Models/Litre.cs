using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace sms.Models
{
    public partial class Litre
    {
        public int LitreId { get; set; }
        public int MonthId { get; set; }
        public int YearId { get; set; }
        public string ParentId { get; set; } 
        public decimal Birr { get; set; }
        public decimal Litre1 { get; set; }
        public decimal Awaited { get; set; } = 0;
        public decimal Totallitre { get; set; }
        public decimal Startkm { get; set; }
        public decimal Endkm { get; set; }
        public decimal Differencekm { get; set; }
        public decimal? Litreused { get; set; } = 0;
        public decimal Birrused { get; set; }
        public decimal Litreremain { get; set; } = 0;
        public decimal? Birrremain { get; set; } = 0;
        public string? Description { get; set; }
        public DateTime RegisteredDate { get; set; }

        public int GeneralId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual General General { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Month Month { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Parent Parent { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Year Year { get; set; }


        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ApplicationUser User { get; set; }
    }
}
