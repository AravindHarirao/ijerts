using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJERTS.Objects
{
    public class Paper : IJERTSAbstract
    {
        public int PaperId { get; set; }
        
        public string MainTitle { get; set; }
        public string ShortDesc { get; set; }
        public string Subject { get; set; }
        public string Tags { get; set; }
        public string PaperPath { get; set; }
        public string FileName { get; set; }

        public string DeclarationPaperPath { get; set; }
        public string DeclarationFileName { get; set; }

        public int IsActive { get; set; }

        public List<PaperAuthors> Authors { get; set; }

        public PaperStatus Status { get; set; }

        public PaperComments Comments { get; set; }
    }
}
