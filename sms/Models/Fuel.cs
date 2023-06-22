using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace sms.Models
{
    public partial class Fuel
    {
        public Fuel()
        {
            Generals = new HashSet<General>();
        }

        public int FuelId { get; set; }
        public string FuelName { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<General> Generals { get; set; }
    }
}
