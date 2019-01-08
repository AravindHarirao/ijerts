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


        // GET: /<controller>/
        [HttpGet]
        public ActionResult Index()
        {
            ActionResult result = this.CheckEditorToken();
            if (result != null)
            {
                return result;
            }
            List<Paper> papers = _editor.GetAllPapers();
            ViewBag.UserType = "Editor";
            return View("Dashboard", papers);
        }
        public ActionResult Authenticate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ValidateLogin(Users users)
        {
            //TODO - Need to pass the Session
            users.SessionId = Guid.NewGuid().ToString();
            users.Password = IJERTSEncryptioncs.Encrypt(users.Password, CommonHelper.SaltPassword, CommonHelper.EncryptKey);
            Users objUsers = _login.ValidateEditorLogin(users);
            if (!string.IsNullOrEmpty(objUsers.Email))
            {
                if (IJERTSEncryptioncs.Encrypt(users.Password, CommonHelper.SaltPassword, CommonHelper.EncryptKey).Equals(objUsers.Password))
                {
                    TempData["UserLoginFailed"] = "Invalid Password. Please try again.";
                    return View("Login");
                }
                else
                {
                    HttpContext.Session["UserId"] = objUsers.UserId.ToString();
                    HttpContext.Session["FirstName"] = objUsers.FirstName.ToString();
                    HttpContext.Session["LastName"] = objUsers.LastName.ToString();
                    //var claims = new List<Claim>
                    //{
                    //    new Claim(ClaimTypes.Email, objUsers.Email)
                    //};

                    //ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                    //ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    //await HttpContext.SignInAsync(principal);
                    UserLoginHistory userLoginHistory = new UserLoginHistory();
                    userLoginHistory.UserId = objUsers.UserId;
                    userLoginHistory.SessionId = HttpContext.Session.SessionID;
                    _login.InsertLoginHistory(userLoginHistory);

                    TempData["UserLoginFailed"] = "Logged in Successfully.";
                    return RedirectToAction("Index", "Author");
                }
            }
            else
            {
                TempData["UserLoginFailed"] = "Invalid Username or Password. Please try again.";
                return View("Login");
            }
        }


        public ActionResult UpdatePaper(Paper paper, string txtComments, string Approver)
        {
            string txt = txtComments;
            string app = Approver;
            return View();
        }

        public ActionResult GetPaperDetails(int id)
        {
            Paper paper = new Paper();

            ActionResult result = this.CheckEditorToken();
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
            //ActionResult result = this.CheckToken();
            //if (result != null)
            //{
            //    return result;
            //}

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
            //ActionResult result = this.CheckToken();
            //if (result != null)
            //{
            //    return result;
            //}

            user.UserActivationValue = Guid.NewGuid().ToString();
            user.Password = IJERTSEncryptioncs.Encrypt(CommonHelper.GenerateDynamicPassword(), CommonHelper.SaltPassword, CommonHelper.EncryptKey);

            _editor.Register(user);

            EmailHelper.SendWelcomeEmailtoUser(user);

            return View("CompleteRegister");
        }

        public FileResult Download(string file)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(file);
            var response = new FileContentResult(fileBytes, "application/octet-stream");
            response.FileDownloadName = Path.GetFileName(file) ;
            return response;
        }

        public ActionResult EditorialBoard()
        {
            return View();
        }
    }
}
