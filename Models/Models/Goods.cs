using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Goods
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specifications { get; set; }

        public int OrderId { get; set; }
        [Navigate(NavigateType.OneToOne, nameof(OrderId))]
        public PurchaseOrder? Order { get; set; }
    }
}
