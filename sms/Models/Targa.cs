using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace sms.Models
{
    public partial class Targa
    {
        public Targa()
        {
            Generals = new HashSet<General>();
        }

        public int TargaId { get; set; }
        public string TargaName { get; set; } 
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<General> Generals { get; set; }


        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ApplicationUser User { get; set; }
    }
}
