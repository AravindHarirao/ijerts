using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJERTS.Objects
{
    public class PaperStatus
    {
        public int StatusID { get; set; }
        public int PaperId { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDatetime { get; set; }
    }
}
