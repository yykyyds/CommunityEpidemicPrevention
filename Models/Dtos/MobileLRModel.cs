using Models.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class MobileLRModel
    {
        public string Openid { get; set; } = default!;
        public string NickName { get; set; } = default!;
        public gender Gender { get; set; }
        public string AvatarUrl { get; set; } = default!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public role Role { get; set; } = role.普通用户;
    }
}
