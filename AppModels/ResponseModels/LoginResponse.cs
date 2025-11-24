using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModels.ResponseModels
{
    public class LoginResponse
    {
        public object Data { get; set; }
        public string Token { get; set; }
        public string Message { get; set;}
        public string Txt { get; set; }
    }
}
