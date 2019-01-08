using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJERTS.Objects
{
    public class LoginResponse : IJERTSAbstract
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public string TokenSetTime { get; set; }
        public string TokenLife { get; set; }
    }
}
