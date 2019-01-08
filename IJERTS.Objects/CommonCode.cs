using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJERTS.Objects
{
    public class CommonCode : IJERTSAbstract
    {
        public int Id { get; set; }

        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public string CodeName { get; set; }
    }
}
