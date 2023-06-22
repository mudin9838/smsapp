using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace sms.Models
{
    public partial class Garage
    {
        public Garage()
        {
            Services = new HashSet<Service>();
        }

        public int GarageId { get; set; }
        public string GarageName { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Service> Services { get; set; }
    }
}
