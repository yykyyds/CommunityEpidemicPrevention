using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class EscalationInfoDto
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string CurrentLocation { get; set; } = default!;
        public healthStasus HealthStasus { get; set; }
        public healthCodeColor HealthCodeColor { get; set; }
        public travelCardStatus TravelCardStatus { get; set; }
        public string? UserName { get; set; }
    }
}
