using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class EchartsModel
    {
        public int EntryCount { get; set; }
        public int OutCount { get; set; }
        public int PurchaseCount { get; set; }
        public string Time { get; set; } = default!;
    }
}
