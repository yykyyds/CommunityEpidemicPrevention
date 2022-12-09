using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class GetAllApplyByUserId
    {
        public EntrysByUserId EntryData { get; set; }
        public OutsByUserId OutData { get; set; }
        public PurchaseByUserId PurchaseData { get; set; }
    }
}
