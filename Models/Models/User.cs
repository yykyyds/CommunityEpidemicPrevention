using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class User
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string OpenId { get; set; }
        [SugarColumn(ColumnDataType = "nvarchar(30)")]
        [Required(ErrorMessage = "用户名不能为空！")]
        [StringLength(maximumLength:6,MinimumLength = 3,ErrorMessage ="用户名长度在3~6个字符之间")]
        public string NickName { get; set; }
        [SugarColumn(ColumnDataType = "nvarchar(100)",IsNullable = true)]
        [StringLength(10, ErrorMessage = "密码长度在6~10个字符之间", MinimumLength = 6)]
        [Required(ErrorMessage = "密码不能为空！")]
        public string PasswordHash { get; set; }
        [Required(ErrorMessage = "确认密码不能为空！")]
        [Compare("PasswordHash",ErrorMessage = "两次密码不一致")]
        [SugarColumn(IsIgnore = true)]
        public string? RePasswordHash { get; set; }
        public gender Gender { get; set; }
        public string AvatarUrl { get; set; }
        [SugarColumn(IsNullable = true)]
        public string? Address { get; set; }
        [SugarColumn(IsNullable = true)]
        public string? Email { get; set; }
        [SugarColumn(IsNullable = true)]
        public string? Phone { get; set; }
        public role Role { get; set; }
        [SugarColumn(ColumnDataType = "nvarchar(MAX)", IsNullable = true)]
        public string? HealthCode { get; set; }
        [SugarColumn(ColumnDataType = "nvarchar(MAX)", IsNullable = true)]
        public string? TripCode { get; set; }
        [SugarColumn(ColumnDataType = "nvarchar(MAX)", IsNullable = true)]
        public string? NucleicAcidReport { get; set; }
        public bool IsOut { get; set; }
        [SugarColumn(IsIgnore = true)]
        public bool IsPersistent { get; set; }

        [Navigate(NavigateType.OneToMany,nameof(EntryRecord.UserId))]
        public List<EntryRecord>? entryRecords { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(OutRecord.UserId))]
        public List<OutRecord>? outRecords { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(PurchaseOrder.UserId))]
        public List<PurchaseOrder>? purchaseOrders { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(EscalationInfo.UserId))]
        public List<EscalationInfo>? escalationInfos { get; set; }
    }

}
