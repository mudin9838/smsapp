using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
namespace sms.Models
{
    public partial class Year
    {
        public Year()
        {
            Litres = new HashSet<Litre>();
            Months = new HashSet<Month>();
        }

        [Key]
        public int YearId { get; set; }

        public string YearName { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Litre> Litres { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Month> Months { get; set; }
    }
}
