using IJERTS.BLL;
using IJERTS.Common;
using IJERTS.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace IJERTS.Web.Controllers
{
    public class EditorController : BaseController
    {
        IEditor _editor = new Editor();
        ILogin _login = new Login();
        IReview _review = new Review();
        
        // GET: /<controller>/
        [HttpGet]
        public ActionResult Index()
        {
            ActionResult result = this.ValidateEditorToken();
            if (result != null)
            {
                return result;
            }

            //Getting the Papers
            List<Paper> papers = _editor.GetAllPapers();

            //Getting the Reviwer 
            List<Users> reviewers = _review.GetAllReviewers();

            List<Queries> queries = _editor.GetQueries();

            var objResults = new Tuple<List<Paper>, List<Users>, List<Queries>>(papers, reviewers, queries);

            ViewBag.UserType = "Editor";
            return View("Dashboard", objResults);
        }
        
        public ActionResult UpdatePaper(Paper paper, int txtPaperId, string txtComments, string Approver)
        {
            _editor.PostComments(txtPaperId, txtComments, int.Parse(HttpContext.Session["UserId"].ToString()));
            TempData["PaperPostingFailed"] = "Comments posted successfully";
            return View("PaperDetails", paper);
        }

        public ActionResult GetPaperDetails(int id)
        {
            Paper paper = new Paper();

            ActionResult result = this.ValidateEditorToken();
            if (result != null)
            {
                return result;
            }

            paper = _editor.GetPaperDetails(id);
            return View("PaperDetails", paper);
        }

        [HttpGet]
        public ActionResult Register()
        {
            CommonCode commonCode = new CommonCode();
            commonCode.GroupId = 5;
            List<Tuple<int, string>> lstSpecialization = _editor.GetSpecialization(commonCode);
            List<SelectListItem> lstSpec = new List<SelectListItem>();
            foreach (var specialization in lstSpecialization)
            {
                lstSpec.Add(new SelectListItem { Value = specialization.Item1.ToString(), Text = specialization.Item2 });
            }
            ViewBag.Specialization = new SelectList(lstSpec, "Value", "Text");
            
            return View();
        }

        [HttpPost]
        public ActionResult Register(Users user)
        {
            user.UserActivationValue = Guid.NewGuid().ToString();
            user.Password = IJERTSEncryptioncs.Encrypt(CommonHelper.GenerateDynamicPassword(), CommonHelper.SaltPassword, CommonHelper.EncryptKey);

            _editor.Register(user);

            EmailHelper.SendWelcomeEmailtoUser(user);

            return View("CompleteRegister");
        }

        public FileResult DownloadAuthorPaper(Int32 PaperId, string PaperFileName)
        {
            string sUploadedPath = Server.MapPath("~/UploadedFiles/AuthorPapers/");
            string sFileName = sUploadedPath + PaperFileName;
            if(!string.IsNullOrEmpty(sFileName))
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

        public ActionResult EditorialBoard()
        {
            return View();
        }

    }
}
