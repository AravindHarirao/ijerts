using IJERTS.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJERTS.DAL
{
    public interface IReviewRepository
    {
        void Register(Users user);

        List<Tuple<int, string>> GetSpecialization(CommonCode commonCode);

        List<Users> GetAllReviewers();

        void ActivateDeActivateReviewer(Users user);
        
        Users GetReviewerDetails(Int32 userId);
    }
}
