using IJERTS.BLL;
using IJERTS.Common;
using IJERTS.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace IJERTS.Web.Controllers
{
    public class AuthorController : BaseController
    {

        IAuthor _author = new Author();

        // GET: /<controller>/
        [HttpGet]
        public ActionResult Index()
        {
            //ActionResult result = this.CheckToken();
            //if (result != null)
            //{
            //    return result;
            //}
            ViewBag.UserType = "Author";
            return View();
        }
        
        [HttpGet]
        public ActionResult Register()
        {
            //ActionResult result = this.CheckToken();
            //if (result != null)
            //{
            //    return result;
            //}

            return View();
        }

        [HttpPost]
        public ActionResult Register(Users user)
        {
            //ActionResult result = this.CheckToken();
            //if (result != null)
            //{
            //    return result;
            //}

            user.UserActivationValue = Guid.NewGuid().ToString();
            user.Password = IJERTSEncryptioncs.Encrypt(CommonHelper.GenerateDynamicPassword(), CommonHelper.SaltPassword, CommonHelper.EncryptKey);

            _author.Register(user);

            EmailHelper.SendWelcomeEmailtoUser(user);

            return View("CompleteRegister");
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult PostPaper()
        {
            ActionResult result = this.CheckToken();
            if (result != null)
            {
                return result;
            }

            return View();
        }

        public ActionResult PublicationCharges()
        {
            return View();
        }

        public ActionResult AuthorResponsibility()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult PostPaper(Paper paper, HttpPostedFileBase PaperPath,
            string AuthorFirstName1, string AuthorLastName1, string AuthorDepartment1, string AuthorOrganisation1,
            string AuthorFirstName2, string AuthorLastName2, string AuthorDepartment2, string AuthorOrganisation2,
            string AuthorFirstName3, string AuthorLastName3, string AuthorDepartment3, string AuthorOrganisation3,
            string AuthorFirstName4, string AuthorLastName4, string AuthorDepartment4, string AuthorOrganisation4)
        {
            ActionResult result = this.CheckToken();
            if (result != null)
            {
                return result;
            }

            Paper newPaper = new Paper();
            List<PaperAuthors> authors = new List<PaperAuthors>();
            paper.CreatedBy = HttpContext.Session["FirstName"].ToString();
            paper.UpdatedBy = HttpContext.Session["FirstName"].ToString();
            newPaper = paper;

            string uploadedPath = Path.Combine(Server.MapPath("../"), "UploadedFiles");
            string filePath = Path.Combine(uploadedPath, PaperPath.FileName);
                        

            paper.FileName = Path.GetFileName(PaperPath.FileName);
            paper.PaperPath = uploadedPath;

            if (!string.IsNullOrEmpty(AuthorFirstName1))
            {
                PaperAuthors paperAuthors1 = new PaperAuthors();
                paperAuthors1.AuthorFirstName = AuthorFirstName1;
                paperAuthors1.AuthorLastName = AuthorLastName1;
                paperAuthors1.Department = AuthorDepartment1;
                paperAuthors1.Organisation = AuthorDepartment1;
                authors.Add(paperAuthors1);
            }

            if (!string.IsNullOrEmpty(AuthorFirstName2))
            {
                PaperAuthors paperAuthors2 = new PaperAuthors();
                paperAuthors2.AuthorFirstName = AuthorFirstName2;
                paperAuthors2.AuthorLastName = AuthorLastName2;
                paperAuthors2.Department = AuthorDepartment2;
                paperAuthors2.Organisation = AuthorDepartment2;
                authors.Add(paperAuthors2);
            }

            if (!string.IsNullOrEmpty(AuthorFirstName3))
            {
                PaperAuthors paperAuthors3 = new PaperAuthors();
                paperAuthors3.AuthorFirstName = AuthorFirstName3;
                paperAuthors3.AuthorLastName = AuthorLastName3;
                paperAuthors3.Department = AuthorDepartment3;
                paperAuthors3.Organisation = AuthorDepartment3;
                authors.Add(paperAuthors3);
            }

            if (!string.IsNullOrEmpty(AuthorFirstName4))
            {
                PaperAuthors paperAuthors4 = new PaperAuthors();
                paperAuthors4.AuthorFirstName = AuthorFirstName4;
                paperAuthors4.AuthorLastName = AuthorLastName4;
                paperAuthors4.Department = AuthorDepartment4;
                paperAuthors4.Organisation = AuthorDepartment4;
                authors.Add(paperAuthors4);
            }

            newPaper.Authors = authors;
            newPaper.UserId = int.Parse(HttpContext.Session["UserId"].ToString());
            _author.PostPapers(paper);
            PaperPath.SaveAs(Path.Combine(paper.PaperPath, paper.FileName));

            return View();
        }

        public ActionResult TrackMyPaper()
        {
            List<Paper> papers = new List<Paper>();

            ActionResult result = this.CheckToken();
            if (result != null)
            {
                return result;
            }

            papers = _author.TrackMyPaper(int.Parse(HttpContext.Session["UserId"].ToString()));
            return View(papers);
        }

    }
}
