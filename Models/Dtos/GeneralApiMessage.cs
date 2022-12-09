using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class GeneralApiMessage
    {
        public int code { get; set; }
        public string? msg { get; set; }
        public dynamic? data { get; set; }
    }
}
