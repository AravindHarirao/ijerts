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
        Users Register(Users user);

        List<Tuple<int, string>> GetSpecialization(CommonCode commonCode);

        List<Users> GetAllReviewers();

        List<Users> GetAllReviewersForPaper(int paperId);


        void ActivateDeActivateReviewer(Users user);

        Users GetReviewerDetails(Int32 userId);

        List<Paper> GetAssignedPaper(Int32 userId);

        int UpdatePaperStatus(int userId, int paperId, string Comments, string Approve);

        Users GetMyProfileDetails(Int64 UserId);

        Users UpdateProfile(Users user);
    }
}
