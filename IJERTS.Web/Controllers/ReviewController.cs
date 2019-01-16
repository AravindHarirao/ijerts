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
    public class ReviewController : BaseController
    {
        IReview _review = new Review();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Responsiblities()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            CommonCode commonCode = new CommonCode();
            List<Tuple<int, string>> lstSpecialization = _review.GetSpecialization(commonCode);
            List<SelectListItem> lstSpec = new List<SelectListItem>();
            foreach (var specialization in lstSpecialization)
            {
                lstSpec.Add(new SelectListItem { Value = specialization.Item1.ToString(), Text = specialization.Item2 });
            }
            ViewBag.Specialization = new SelectList(lstSpec, "Value", "Text");

            return View();
        }

        [HttpPost]
        public ActionResult Register(Users user, HttpPostedFileBase UploadResume)
        {
            user.UserType = "R";

            string uploadedPath = Server.MapPath("~/UploadedFiles/ReviewerResume");

            bool exists = Directory.Exists(uploadedPath);
            if (!exists)
                Directory.CreateDirectory(uploadedPath);

            string sUploadFileName = DateTime.Now.Ticks.ToString();

            string filePath = Path.Combine(uploadedPath, UploadResume.FileName);

            user.ResumeFileName = Path.GetFileName(UploadResume.FileName);

            UploadResume.SaveAs(Path.Combine(uploadedPath, user.ResumeFileName));

            _review.Register(user);

            TempData["ReviewerRegisterHeading"] = "Registration Completed!";
            TempData["ReviewerRegisterMessage"] = "Thank you for registering with us. Your Registration is successfull and you can Review after approval.";

            return View("CompleteRegister");
        }

        [HttpPost]
        public ActionResult ActivateDeActivateReviewer(Int32 UserId, string FirstName, string LastName, string Email, string Type)
        {
            Users objUser = new Users();

            objUser.UserId = UserId;
            objUser.FirstName = FirstName;
            objUser.LastName = LastName;
            objUser.Email = Email;
            objUser.UpdatedBy = Session["FirstName"].ToString();

            //Activate Reviewer
            if(!string.IsNullOrEmpty(Type) && Type.ToUpper().Equals("A"))
            {
                objUser.UserActivated = true;
                objUser.UserActivationValue = Guid.NewGuid().ToString();
                objUser.Password = IJERTSEncryptioncs.Encrypt(CommonHelper.GenerateDynamicPassword(), CommonHelper.SaltPassword, CommonHelper.EncryptKey);

                EmailHelper.SendWelcomeEmailtoReviewer(objUser);

                _review.ActivateDeActivateReviewer(objUser);

                TempData["ReviewerRegisterHeading"] = "Registration Activated!";
                TempData["ReviewerRegisterMessage"] = string.Format("Reviewer {0} {1} registration is Activated successfully.", FirstName, LastName);
            }
            //DeActivate Reviewer
            else
            {
                objUser.UserActivated = false;
                objUser.UserActivationValue = "False";
                objUser.Password = "";
                //EmailHelper.SendWelcomeEmailtoReviewer(objUser);

                _review.ActivateDeActivateReviewer(objUser);

                TempData["ReviewerRegisterHeading"] = "Registration De-Activated!";
                TempData["ReviewerRegisterMessage"] = string.Format("Reviewer {0} {1} registration is De-Activated successfully.", FirstName, LastName);
            }

            return View("CompleteRegister");
        }
        
        public ActionResult GetReviewerDetails(Int32 userId)
        {
            Users users = new Users();

            ActionResult result = this.ValidateEditorToken();
            if (result != null)
            {
                return result;
            }

            users = _review.GetReviewerDetails(userId);
            return View("ReviewDetails", users);
        }

        public FileResult DownloadResume(Int32 UserId, string ResumeFileName)
        {
            string sUploadedPath = Server.MapPath("~/UploadedFiles/ReviewerResume/");
            string sFileName = sUploadedPath + ResumeFileName;
            if (!string.IsNullOrEmpty(sFileName))
            {
                if (System.IO.File.Exists(sFileName))
                {
                    string fileExtension = Path.GetExtension(sFileName);
                    return this.File(sFileName, "application/" + fileExtension, ResumeFileName);
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