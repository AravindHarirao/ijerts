using IJERTS.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJERTS.BLL
{
    public interface IReview
    {
        void Register(Users user);

        List<Tuple<int, string>> GetSpecialization(CommonCode commonCode);

        List<Users> GetAllReviewers();

        void ActivateDeActivateReviewer(Users user);

        Users GetReviewerDetails(Int32 userId);

        List<Paper> GetAssignedPaper(Int32 userId);

        int UpdatePaperStatus(int userId, int paperId, string Comments, string Approve);

    }
}
