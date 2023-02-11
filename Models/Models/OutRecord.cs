using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class OutRecord
    {
        [SugarColumn(IsPrimaryKey = true,IsIdentity = true)]
        public int Id { get; set; }
        public string Destination { get; set; } = default!;
        public DateTime OutTime { get; set; }
        public status Status { get; set; } = status.审核中;

        public string UserId { get; set; } = default!;
        [Navigate(NavigateType.OneToOne, nameof(UserId))]
        public User? user { get; set; }
    }
}
