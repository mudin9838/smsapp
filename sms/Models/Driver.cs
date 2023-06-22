using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace sms.Models
{
    public partial class Driver
    {
        public Driver()
        {
            Generals = new HashSet<General>();
        }

        public int DriverId { get; set; }
        public string DriverName { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<General> Generals { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ApplicationUser User { get; set; }
    }
}
