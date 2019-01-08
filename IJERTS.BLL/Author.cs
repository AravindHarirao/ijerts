using IJERTS.DAL;
using IJERTS.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJERTS.BLL
{
    public class Author : IAuthor
    {

        IAuthorRepository _authorRepo = new AuthorRepository();

        public void Register(Users user)
        {
            _authorRepo.Register(user);
        }

        public void GetAllUsers(List<Users> users)
        {
            throw new NotImplementedException();
        }

        public int PostPapers(Paper paper)
        {
            return _authorRepo.PostPapers(paper);
        }

        public List<Paper> TrackMyPaper(int userId)
        {
            return _authorRepo.TrackMyPaper(userId);
        }

    }
}
