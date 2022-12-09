using Models.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class UserUpload
    {
        public string OpenId { get; set; }
        public string TripCode { get; set; }
        public string HealthCode { get; set; }
        public string NucleicAcidReport { get; set; }
    }
}
