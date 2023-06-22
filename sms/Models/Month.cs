using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace sms.Models
{
    public partial class Month
    {
        public Month()
        {
            Litres = new HashSet<Litre>();
        }

        public int MonthId { get; set; }
        public string MonthName { get; set; }
        public int YearId { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Year? Year { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Litre> Litres { get; set; }
    }
}
