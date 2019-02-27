using IJERTS.BLL;
using IJERTS.Common;
using IJERTS.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            ViewBag.UserType = "Author";
            return View();
        }
        
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Users user)
        {
            Users objUser = new Users();

            user.UserType = "A";
            user.UserActivationValue = Guid.NewGuid().ToString();
            user.Password = CommonHelper.GenerateDynamicPassword();

            ValidationContext context = new ValidationContext(user, null, null);
            List<ValidationResult> results = new List<ValidationResult>();
            bool valid = Validator.TryValidateObject(user, context, results, true);

            if (!valid)
            {
                foreach (ValidationResult vr in results)
                {
                    ViewData["ErrorMessage"] = vr.ErrorMessage;
                    
                }
                return View();
            }
            else
            {
                objUser = _author.Register(user);

                if (objUser.ResultMessage.ToUpper().Equals("SUCCESS"))
                {
                    EmailHelper.SendWelcomeEmailtoUser(user);

                    TempData["AuthorRegisterHeading"] = "Registration Completed!";
                    TempData["AuthorRegisterMessage"] = "Thank you for registering with us. Your Registration is successfull and you can check your email for your login credentials.";

                    return View("CompleteRegister");
                }
                else
                {
                    TempData["AuthorUserExists"] = "Author Email Address already registered with us. Please try with another Email Address...";
                    return View();
                }
            }
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult PostPaper()
        {
            ActionResult result = this.ValidateAuthorToken();
            if (result != null)
            {
                return result;
            }
            List<Specialisation> specialisations = _author.GetSpecialisation();

            ViewData["Subject"] = new SelectList ((System.Collections.IEnumerable)specialisations, "specialisation", "specialisation");

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
        public ActionResult PostPaper(Paper paper, HttpPostedFileBase PaperPath, HttpPostedFileBase DeclarationPaperPath,
            string AuthorFirstName1, string AuthorLastName1, string AuthorDepartment1, string AuthorOrganisation1,
            string AuthorFirstName2, string AuthorLastName2, string AuthorDepartment2, string AuthorOrganisation2,
            string AuthorFirstName3, string AuthorLastName3, string AuthorDepartment3, string AuthorOrganisation3,
            string AuthorFirstName4, string AuthorLastName4, string AuthorDepartment4, string AuthorOrganisation4)
        {
            ActionResult result = this.ValidateAuthorToken();
            if (result != null)
            {
                return result;
            }

            List<Specialisation> specialisations = _author.GetSpecialisation();
            ViewData["Subject"] = new SelectList((System.Collections.IEnumerable)specialisations, "specialisation", "specialisation");


            try
            {
                Paper newPaper = new Paper();
                List<PaperAuthors> authors = new List<PaperAuthors>();
                paper.CreatedBy = HttpContext.Session["FirstName"].ToString();
                paper.UpdatedBy = HttpContext.Session["FirstName"].ToString();
                newPaper = paper;

                string uploadedPath = Server.MapPath("~/UploadedFiles/AuthorPapers/");                
                string sPaperPathName = Path.GetFileName(PaperPath.FileName);
                paper.FileName = string.Format("{0}-{1}{2}", Path.GetFileNameWithoutExtension(sPaperPathName), Guid.NewGuid().ToString("N"), Path.GetExtension(sPaperPathName));
                paper.PaperPath = uploadedPath;

                string declarationPath = Server.MapPath("~/UploadedFiles/Declaration/");
                string sDecFileName = Path.GetFileName(DeclarationPaperPath.FileName);
                paper.DeclarationFileName = string.Format("{0}-{1}{2}", Path.GetFileNameWithoutExtension(sDecFileName), Guid.NewGuid().ToString("N"), Path.GetExtension(sDecFileName));
                paper.DeclarationPaperPath = declarationPath;

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

                if(!string.IsNullOrEmpty(paper.PaperPath))
                {
                    PaperPath.SaveAs(Path.Combine(paper.PaperPath, paper.FileName));
                }

                if (!string.IsNullOrEmpty(paper.DeclarationPaperPath))
                {
                    DeclarationPaperPath.SaveAs(Path.Combine(paper.DeclarationPaperPath, paper.DeclarationFileName));
                }

                ViewData["PaperPostingFailed"] = "Paper posted successfully.";
            }
            catch (Exception ex)
            {
                ViewData["PaperPostingFailed"] = "Error in posting paper. Please contact administrator";
            }

            ModelState.Clear();
            return View();
        }

        public ActionResult TrackMyPaper()
        {
            List<Paper> papers = new List<Paper>();

            ActionResult result = this.ValidateAuthorToken();
            if (result != null)
            {
                return result;
            }

            papers = _author.TrackMyPaper(int.Parse(HttpContext.Session["UserId"].ToString()));
            return View(papers);
        }

        public ActionResult GetPaperDetails(int id)
        {
            Paper paper = new Paper();

            ActionResult result = this.ValidateAuthorToken();
            if (result != null)
            {
                return result;
            }

            paper = _author.GetPaperDetails(id);
            return View("PaperDetails", paper);
        }

        [HttpPost]
        public ActionResult GetPaperDetails(HttpPostedFileBase UpdatedPaper, int txtPaperId)
        {
            Paper paper = new Paper();
            try
            {
                paper.PaperId = txtPaperId;
                string uploadedPath = Server.MapPath("~/UploadedFiles/AuthorPapers/");
                string sFileName = Path.GetFileName(UpdatedPaper.FileName);
                paper.FileName = string.Format("{0}-{1}{2}", Path.GetFileNameWithoutExtension(sFileName), Guid.NewGuid().ToString("N"), Path.GetExtension(sFileName));
                paper.PaperPath = uploadedPath;
                paper.CreatedBy = HttpContext.Session["FirstName"].ToString();
                paper.UpdatedBy = HttpContext.Session["FirstName"].ToString();

                if (!string.IsNullOrEmpty(paper.PaperPath))
                {
                    UpdatedPaper.SaveAs(Path.Combine(paper.PaperPath, paper.FileName));
                }

                paper = _author.UpdatePaperDetails(paper);               
                ViewData["PaperPostingFailed"] = "Paper posted successfully.";                
            }
            catch (Exception ex)
            {
                ViewData["PaperPostingFailed"] = "Error in posting paper. Please contact administrator";
            }
            return View("PaperDetails", paper);
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult MyProfile()
        {
            TempData["AuthorUpdated"] = "";
            Users users = new Users();
            ActionResult result = this.ValidateAuthorToken();
            if (result != null)
            {
                return result;
            }
            if(Session["UserId"] == null)
            {
                return RedirectToAction("AuthorLogin", "Login");
            }
            users = _author.GetMyProfileDetails(Convert.ToInt64(Session["UserId"].ToString()));
            return View("MyProfile", users);
        }

        [HttpPost]
        public ActionResult UpdateProfile(Users user)
        {
            Users users = new Users();
            ActionResult result = this.ValidateAuthorToken();
            if (result != null)
            {
                return result;
            }
            users = _author.UpdateProfile(user);
            TempData["AuthorUpdated"] = "Record updated successfully...";
            return View("MyProfile", users);
        }

        public FileResult DownloadAuthorPaper(Int32 PaperId, string PaperFileName)
        {
            string sUploadedPath = Server.MapPath("~/UploadedFiles/AuthorPapers/");
            string sFileName = sUploadedPath + PaperFileName;
            if (!string.IsNullOrEmpty(sFileName))
            {
                if (System.IO.File.Exists(sFileName))
                {
                    string fileExtension = Path.GetExtension(sFileName);
                    return this.File(sFileName, "application/" + fileExtension, PaperFileName);
                }
                else
                {
                    return this.File(Path.Combine(Server.MapPath("~/UploadedFiles/"), "FileNotExists.txt"), "application/txt", "FileNotExists.txt");
                }
            }
            else
            {
                return this.File(Path.Combine(Server.MapPath("~/UploadedFiles/"), "FileNotExists.txt"), "application/txt", "FileNotExists.txt");
            }
        }

        public FileResult DownloadDeclaration(Int32 PaperId, string DeclarationPaperfilename)
        {
            string sUploadedPath = Server.MapPath("~/UploadedFiles/Declaration/");
            string sFileName = sUploadedPath + DeclarationPaperfilename;
            if (!string.IsNullOrEmpty(sFileName))
            {
                if (System.IO.File.Exists(sFileName))
                {
                    string fileExtension = Path.GetExtension(sFileName);
                    return this.File(sFileName, "application/" + fileExtension, DeclarationPaperfilename);
                }
                else
                {
                    return this.File(Path.Combine(Server.MapPath("~/UploadedFiles/"), "FileNotExists.txt"), "application/txt", "FileNotExists.txt");
                }
            }
            else
            {
                return this.File(Path.Combine(Server.MapPath("~/UploadedFiles/"), "FileNotExists.txt"), "application/txt", "FileNotExists.txt");
            }
        }

    }
}
