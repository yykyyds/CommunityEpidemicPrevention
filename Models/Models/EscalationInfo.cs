using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    //上报信息
    public class EscalationInfo
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public string CurrentLocation { get; set; }
        public healthStasus HealthStasus { get; set; }
        public healthCodeColor HealthCodeColor { get; set; }
        public travelCardStatus TravelCardStatus { get; set; }

        public string UserId { get; set; }
        [Navigate(NavigateType.OneToOne, nameof(UserId))]
        public User? user { get; set; }
    }
}
