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

            TempData["ReviewerUserExists"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult Register(Users user, HttpPostedFileBase UploadResume)
        {
            Users objUser = new Users();

            user.UserType = "R";

            if (UploadResume != null)
            {
                string uploadedPath = Server.MapPath("~/UploadedFiles/ReviewerResume");

                bool exists = Directory.Exists(uploadedPath);
                if (!exists)
                    Directory.CreateDirectory(uploadedPath);

                string sUploadFileName = DateTime.Now.Ticks.ToString() + Path.GetFileName(UploadResume.FileName);

                string filePath = Path.Combine(uploadedPath, sUploadFileName);

                user.ResumeFileName = sUploadFileName;

                UploadResume.SaveAs(Path.Combine(uploadedPath, user.ResumeFileName));
            }

            objUser = _review.Register(user);

            if(objUser.ResultMessage.ToUpper().Equals("SUCCESS"))
            {
                TempData["ReviewerRegisterHeading"] = "Registration Completed!";
                TempData["ReviewerRegisterMessage"] = "Thank you for registering with us. Your Registration is successfull and you can Review after approval.";

                return View("CompleteRegister");
            }
            else
            {
                CommonCode commonCode = new CommonCode();
                List<Tuple<int, string>> lstSpecialization = _review.GetSpecialization(commonCode);
                List<SelectListItem> lstSpec = new List<SelectListItem>();
                foreach (var specialization in lstSpecialization)
                {
                    lstSpec.Add(new SelectListItem { Value = specialization.Item1.ToString(), Text = specialization.Item2 });
                }
                ViewBag.Specialization = new SelectList(lstSpec, "Value", "Text");

                TempData["ReviewerUserExists"] = "Reviewer Email Address already registered with us. Please try again...";
                return View();
            }
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
                objUser.Password = CommonHelper.GenerateDynamicPassword();

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

        public ActionResult Dashboard()
        {
            IEditor _editor = new Editor();

            ActionResult result = this.ValidateReviewerToken();
            if (result != null)
            {
                return result;
            }

            List<Paper> assignedPapers = new List<Paper>();

            assignedPapers = _review.GetAssignedPaper((int) UserId);

            List<Users> reviewers = _review.GetAllReviewers();

            return View("Dashboard", assignedPapers);

        }

        public ActionResult GetPaperDetails(int id)
        {
            IEditor _editor = new Editor();
            Paper paper = new Paper();

            ActionResult result = this.ValidateReviewerToken();
            if (result != null)
            {
                return result;
            }

            paper = _editor.GetPaperDetails(id);

            List<Users> reviewers = _review.GetAllReviewersForPaper(paper.PaperId);
            ViewData["ApproveStatus"] = new SelectList(
                    new List<SelectListItem>
                                        {
                                            new SelectListItem { Selected = true, Text = string.Empty, Value = "-1"},
                                            new SelectListItem { Selected = false, Text = "Approved", Value = "Approved"},
                                            new SelectListItem { Selected = false, Text = "Rejected", Value = "Rejected"},
                                        }, "Value", "Text", 1); 

            return View("PaperDetails", paper);
        }

        public ActionResult UpdatePaperStatus(string txtPaperId, string txtComments, string ApproveStatus)
        {
            IEditor _editor = new Editor();
            Paper paper = new Paper();

            ApproveStatus = ApproveStatus.Trim().Equals("-1") ? string.Empty : ApproveStatus.Trim();

            _review.UpdatePaperStatus((int)UserId, int.Parse(txtPaperId), txtComments, ApproveStatus);
            paper = _editor.GetPaperDetails(int.Parse(txtPaperId));

            ViewData["ApproveStatus"] = new SelectList(
                                        new List<SelectListItem>
                                                {
                                                                new SelectListItem { Selected = true, Text = string.Empty, Value = "-1"},
                                                                new SelectListItem { Selected = false, Text = "Approved", Value = "Approved"},
                                                                new SelectListItem { Selected = false, Text = "Rejected", Value = "Rejected"},
                                                }, "Value", "Text", 1);

            TempData["UpdateStatusResuslt"] = "Details updated successfully.";
            return View("PaperDetails", paper);
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

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult MyProfile()
        {
            TempData["ReviewerUpdated"] = "";
            Users users = new Users();
            ActionResult result = this.ValidateAuthorToken();
            if (result != null)
            {
                return result;
            }
            if (Session["UserId"] == null)
            {
                return RedirectToAction("AuthorLogin", "Login");
            }

            CommonCode commonCode = new CommonCode();
            List<Tuple<int, string>> lstSpecialization = _review.GetSpecialization(commonCode);
            List<SelectListItem> lstSpec = new List<SelectListItem>();
            foreach (var specialization in lstSpecialization)
            {
                lstSpec.Add(new SelectListItem { Value = specialization.Item1.ToString(), Text = specialization.Item2 });
            }
            ViewBag.Specialization = new SelectList(lstSpec, "Value", "Text");

            users = _review.GetMyProfileDetails(Convert.ToInt64(Session["UserId"].ToString()));
            return View("MyProfile", users);
        }

        [HttpPost]
        public ActionResult UpdateProfile(Users user, HttpPostedFileBase UploadResume)
        {
            Users users = new Users();
            ActionResult result = this.ValidateAuthorToken();
            if (result != null)
            {
                return result;
            }

            if(UploadResume != null)
            {
                string uploadedPath = Server.MapPath("~/UploadedFiles/ReviewerResume");

                bool exists = Directory.Exists(uploadedPath);
                if (!exists)
                    Directory.CreateDirectory(uploadedPath);

                string sUploadFileName = DateTime.Now.Ticks.ToString() + Path.GetFileName(UploadResume.FileName);

                string filePath = Path.Combine(uploadedPath, sUploadFileName);

                user.ResumeFileName = sUploadFileName;

                UploadResume.SaveAs(Path.Combine(uploadedPath, user.ResumeFileName));
            }
            else
            {
                user.ResumeFileName = user.UpdateResumeFileName;
            }

            CommonCode commonCode = new CommonCode();
            List<Tuple<int, string>> lstSpecialization = _review.GetSpecialization(commonCode);
            List<SelectListItem> lstSpec = new List<SelectListItem>();
            foreach (var specialization in lstSpecialization)
            {
                lstSpec.Add(new SelectListItem { Value = specialization.Item1.ToString(), Text = specialization.Item2 });
            }
            ViewBag.Specialization = new SelectList(lstSpec, "Value", "Text");

            users = _review.UpdateProfile(user);
            
            TempData["ReviewerUpdated"] = "Record updated successfully...";
            return View("MyProfile", users);
        }

    }
}