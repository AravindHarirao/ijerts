using IJERTS.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IJERTS.DAL;

namespace IJERTS.BLL
{
    public class Home : IHome
    {

        IHomeRepository homeRepository = new HomeRepository();

        public int PostQuery(Queries query)
        {
            return (homeRepository.PostQuery(query));
        }

        



    }
}
