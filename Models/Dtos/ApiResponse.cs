using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class ApiResponse<T> where T : class
    {
        public int code { get; set; } = 0;
        public string msg { get; set; } = "";
        public int count { get; set; }
        public List<T> data { get; set; }
    }
}
