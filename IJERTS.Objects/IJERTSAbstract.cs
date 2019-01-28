using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJERTS.Objects
{
    public class IJERTSAbstract
    {
        public long UserId { get; set; }

        public int SortOrder { get; set; }

        public int IsActive { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedDatetime { get; set; }

        public string UpdatedBy { get; set; }

        public string SessionId { get; set; }

        public short ResultCode { get; set; }

        public string ResultMessage { get; set; }
    }
}
