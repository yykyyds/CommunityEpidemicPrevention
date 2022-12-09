using Models.Models;
using Org.BouncyCastle.Crypto;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class UsersDto
    {
        public string NickName { get; set; }
        public gender Gender { get; set; }
        public string AvatarUrl { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public role Role { get; set; }
        public bool IsOut { get; set; }
        public string? HealthCode { get; set; }
        public string? TripCode { get; set; }
        public string? NucleicAcidReport { get; set; }
        public List<EscalationInfo>? escalationInfos { get; set; }
    }
}
