using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJERTS.Objects
{
    public class PaperAuthors : IJERTSAbstract
    {
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string Department { get; set; }
        public string Organisation { get; set; }
        public string Subject { get; set; }
    }
}
