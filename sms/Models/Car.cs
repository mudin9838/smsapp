using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace sms.Models
{
    public partial class Car
    {
        public Car()
        {
            Generals = new HashSet<General>();
        }

        public int CarId { get; set; }
        public string CarName { get; set; } = null!;

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<General> Generals { get; set; }
    }
}
