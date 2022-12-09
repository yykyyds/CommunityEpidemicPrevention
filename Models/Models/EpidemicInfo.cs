using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    //疫情信息
    public class EpidemicInfo
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        //确诊
        public int DiagnosisCount { get; set; }
        //无症状
        public int AsymptomaticCount { get; set; }
        public string? Content { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public int Total { get; set; }
    }
}
