using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJERTS.Objects
{
    public class ContactUs : IJERTSAbstract
    {
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string EmailMessage { get; set; }
    }
}
