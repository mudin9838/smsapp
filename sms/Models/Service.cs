using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace sms.Models
{
    public partial class Service
    {
        public int ServiceId { get; set; }

        public string ParentId { get; set; } 
        public string Measurement { get; set; } 
        public int GarageId { get; set; } 
        public int ReplacementId { get; set; }
        public decimal EachPrice { get; set; }
        public decimal Vat { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal km { get; set; }
        public decimal Kmnext { get; set; }
 
       
        public DateTime RegisteredDate { get; set; }

        public int GeneralId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual General General { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Garage Garage { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Parent Parent { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Replacement Replacement { get; set; }


        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ApplicationUser User { get; set; }
    }
}
