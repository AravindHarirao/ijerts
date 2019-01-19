using IJERTS.DAL;
using IJERTS.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJERTS.BLL
{
    public class Editor : IEditor
    {

        IEditorRepository _editorRepository = new EditorRepository();

        public void Register(Users user)
        {
            _editorRepository.Register(user);
        }

        public List<Users> GetAllUsers(Users users)
        {
            return _editorRepository.GetAllUsers(users);
        }

        public List<Tuple<int, string>> GetSpecialization(CommonCode commonCode)
        {
            List<Tuple<int, string>> lstSpecialization = new List<Tuple<int, string>>();
            lstSpecialization = _editorRepository.GetSpecialization(commonCode);
            return lstSpecialization;
        }

        public List<Paper> GetAllPapers()
        {
            return _editorRepository.GetAllPapers();
        }

        public Paper GetPaperDetails(int id)
        {
            return _editorRepository.GetPaperDetails(id);
        }

        public int PostComments(int paperId, string comments, int userId, int approverId)
        {
            return _editorRepository.PostComments(paperId, comments, userId, approverId);
        }

        public List<Queries> GetQueries()
        {
            IHomeRepository homeRepository = new HomeRepository();
            return (homeRepository.GetQueries());
        }
    }
}
