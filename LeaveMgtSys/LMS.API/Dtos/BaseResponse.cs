using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.API.Dtos
{

    public interface IBaseResponse
    {
        public string code { get; set; }
        public string desc { get; set; }
        public object data { get; set; }
        public string token { get; set; }
    }
    public class BaseResponse : IBaseResponse
    {
        public string code { get; set; }
        public string desc { get; set; }
        public object data { get; set; }
        public string token { get; set; }
        public BaseResponse()
        {
            code = "00";
            desc = "Something went wrong";
        }
    }
}
