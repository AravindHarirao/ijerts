using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJERTS.Objects
{
    public class UserLoginHistory : IJERTSAbstract
    {
        public long LoginId { get; set; }

        public DateTime LoggedInTime { get; set; }

        public DateTime? LoggedOutTime { get; set; }
    }
}
