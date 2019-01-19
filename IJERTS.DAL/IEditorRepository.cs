using IJERTS.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJERTS.DAL
{
    public interface IEditorRepository
    {
        void Register(Users user);

        List<Users> GetAllUsers(Users users);

        List<Tuple<int, string>> GetSpecialization(CommonCode commonCode);

        List<Paper> GetAllPapers();

        Paper GetPaperDetails(int id);

        int PostComments(int paperId, string comments, int userId, int approverId);
    }
}
