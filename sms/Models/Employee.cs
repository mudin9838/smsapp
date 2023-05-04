using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

#nullable disable

namespace sms.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Outs = new HashSet<Out>();
        }

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int DepartmentId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Department Department { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Out> Outs { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ApplicationUser User { get; set; }
    }
}
