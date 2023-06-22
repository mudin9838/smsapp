using System.ComponentModel.DataAnnotations;

namespace sms.Models
{
    public class Notification
    {
        [Key]
        public int NotifyId { get; set; }
        public string Notify { get; set; }
    }
}
