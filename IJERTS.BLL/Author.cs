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

        public Users Register(Users user)
        {
            return _authorRepo.Register(user);
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

        public Paper GetPaperDetails(int id)
        {
            return _authorRepo.GetPaperDetails(id);
        }

        public List<Specialisation> GetSpecialisation()
        {
            return _authorRepo.GetSpecialisation();
        }

        public Users GetMyProfileDetails(Int64 UserId)
        {
            return _authorRepo.GetMyProfileDetails(UserId);
        }

        public Users UpdateProfile(Users user)
        {
            return _authorRepo.UpdateProfile(user);
        }
    }
}
