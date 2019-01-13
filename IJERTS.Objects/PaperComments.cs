using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJERTS.Objects
{
    public class PaperComments
    {

        public string CommentsId { get; set; }
        
        public int PaperId { get; set; }

        public string Comments { get; set; }

        public int IsEditorComments { get; set; }

        public int IsActive { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedDateTime { get; set; }


    }
}
