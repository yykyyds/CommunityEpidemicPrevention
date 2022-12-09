using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class GeneralApiResult
    {
        public static GeneralApiMessage Success(string? msg,dynamic? data)
        {
            return new GeneralApiMessage()
            {
                code = 200,
                msg = msg,
                data = data
            };
        }

        public static GeneralApiMessage Error(string? msg, dynamic? data)
        {
            return new GeneralApiMessage()
            {
                code = 500,
                msg = msg,
                data = data
            };
        }
    }
}
