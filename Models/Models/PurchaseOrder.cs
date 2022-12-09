using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class PurchaseOrder
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public DateTime SubmitTime { get; set; } = DateTime.Now;
        public DateTime DeliveryTime { get; set; }
        public string Address { get; set; }
        public status Status { get; set; } = status.审核中;

        [Navigate(NavigateType.OneToMany, nameof(Goods.OrderId))]
        public List<Goods>? goodsList { get; set; }

        public string UserId { get; set; }
        [Navigate(NavigateType.OneToOne,nameof(UserId))]
        public User? user { get; set; }
    }
}
