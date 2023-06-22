using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace sms.Models
{
    public partial class Replacement
    {
        public Replacement()
        {
            Services = new HashSet<Service>();
        }

        public int ReplacementId { get; set; }
        public string ReplacementName { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Service> Services { get; set; }
    }
}
