using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace sms.Models
{
    public partial class General
    {
        public General()
        {
            Litres = new HashSet<Litre>();
        }

        public int GeneralId { get; set; }
        public string ParentId { get; set; } 
        public int CarId { get; set; }
        public int TargaId { get; set; }
        public int DriverId { get; set; }
        public DateTime RegisteredDate { get; set; }
        public int FuelId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Car Car { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Driver Driver { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Fuel Fuel { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Parent Parent { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Targa Targa { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Litre> Litres { get; set; }


        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ApplicationUser User { get; set; }
    }
}
