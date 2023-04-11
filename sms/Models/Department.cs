using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

#nullable disable

namespace sms.Models
{
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
            Outs = new HashSet<Out>();
        }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Employee> Employees { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Out> Outs { get; set; }
    }
}
