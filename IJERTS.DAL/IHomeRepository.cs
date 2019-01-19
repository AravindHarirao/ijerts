using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IJERTS.Objects;

namespace IJERTS.DAL
{
    public interface IHomeRepository
    {
        int PostQuery(Queries query);

    }
}
