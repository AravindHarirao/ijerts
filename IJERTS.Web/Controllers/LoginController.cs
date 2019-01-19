using IJERTS.BLL;
using IJERTS.Common;
using IJERTS.Objects;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IJERTS.Web.Controllers
{
    public class LoginController : BaseController
    {
        ILogin _login = new Login();

        public ActionResult Index()
        {
            return View();
        }

        //[OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult AuthorLogin(string returnUrl = "")
        {
            TempData["UserLoginFailed"] = "";
            return View("AuthorLogin");
        }
        
        public ActionResult EditorLogin(string returnUrl = "")
        {
            TempData["UserLoginFailed"] = "";
            return View("EditorLogin");
        }

        public ActionResult ReviewerLogin(string returnUrl = "")
        {
            TempData["UserLoginFailed"] = "";
            return View("ReviewerLogin");
        }

        [HttpPost]
        public ActionResult ValidateAuthorLogin(Users users)
        {
            //TODO - Need to pass the Session
            users.SessionId = Guid.NewGuid().ToString();
            users.Password = IJERTSEncryptioncs.Encrypt(users.Password, CommonHelper.SaltPassword, CommonHelper.EncryptKey);
            users.UserType = "A";
            Users objUsers = _login.ValidateLogin(users);
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
                    HttpContext.Session["UserType"] = objUsers.UserType.ToString();

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

        [HttpPost]
        public ActionResult ValidateEditorLogin(Users users)
        {
            //TODO - Need to pass the Session
            users.SessionId = Guid.NewGuid().ToString();
            users.Password = IJERTSEncryptioncs.Encrypt(users.Password, CommonHelper.SaltPassword, CommonHelper.EncryptKey);
            users.UserType = "E";
            Users objUsers = _login.ValidateLogin(users);
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
                    HttpContext.Session["UserType"] = objUsers.UserType.ToString();

                    UserLoginHistory userLoginHistory = new UserLoginHistory();
                    userLoginHistory.UserId = objUsers.UserId;
                    userLoginHistory.SessionId = HttpContext.Session.SessionID;
                    _login.InsertLoginHistory(userLoginHistory);

                    TempData["UserLoginFailed"] = "Logged in Successfully.";
                    return RedirectToAction("Index", "Editor");
                }
            }
            else
            {
                TempData["UserLoginFailed"] = "Invalid Username or Password. Please try again.";
                return View("Login");
            }
        }

        [HttpPost]
        public ActionResult ValidateReviewerLogin(Users users)
        {
            //TODO - Need to pass the Session
            users.SessionId = Guid.NewGuid().ToString();
            users.Password = IJERTSEncryptioncs.Encrypt(users.Password, CommonHelper.SaltPassword, CommonHelper.EncryptKey);
            users.UserType = "R";
            Users objUsers = _login.ValidateLogin(users);
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
                    HttpContext.Session["UserType"] = objUsers.UserType.ToString();
                    
                    UserLoginHistory userLoginHistory = new UserLoginHistory();
                    userLoginHistory.UserId = objUsers.UserId;
                    userLoginHistory.SessionId = HttpContext.Session.SessionID;
                    _login.InsertLoginHistory(userLoginHistory);

                    TempData["UserLoginFailed"] = "Logged in Successfully.";
                    return RedirectToAction("Index", "Review");
                }
            }
            else
            {
                TempData["UserLoginFailed"] = "Invalid Username or Password. Please try again.";
                return View("Login");
            }
        }

        [HttpPost]
        public ActionResult ValidateLogin(Users users)
        {
            //TODO - Need to pass the Session
            users.SessionId = Guid.NewGuid().ToString();
            users.Password = IJERTSEncryptioncs.Encrypt(users.Password, CommonHelper.SaltPassword, CommonHelper.EncryptKey);
            Users objUsers = _login.ValidateLogin(users);
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
        
        [HttpGet]
        public  ActionResult AuthenticateActivation()
        {
            string Id = HttpContext.Request.QueryString["Id"].ToString();
            Users user = _login.ValidateUserActivation(Id);
            if (!string.IsNullOrEmpty(user.Email))
            {
                HttpContext.Session["UserId"] = user.UserId.ToString();
                HttpContext.Session["FirstName"] =  user.FirstName.ToString();
                HttpContext.Session["LastName"] = user.LastName.ToString();

                //TempData["UserLoginFailed"] = "Logged in Successfully.";
                return RedirectToAction("Index", "Author");
            }
            else
            {
                TempData["UserLoginFailed"] = "Login Failed.Please enter correct credentials";
                return View("Login");
            }
        }

        [HttpGet]
        public ActionResult UpdateUserSession()
        {
            //TODO - Need to pass the Session
            UserLoginHistory userLoginHistory = new UserLoginHistory();
            userLoginHistory.UserId = Convert.ToInt64(HttpContext.Session["UserId"]);
            userLoginHistory.SessionId = HttpContext.Session.SessionID;
            string sResults = _login.UpdateUserSession(userLoginHistory);

            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("FirstName");
            HttpContext.Session.Remove("LastName");
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Change Password.
        /// </summary>
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(Users users)
        {
            if (string.IsNullOrWhiteSpace(users.CurrentPassword) || string.IsNullOrWhiteSpace(users.NewPassword)
                || string.IsNullOrWhiteSpace(users.ConfirmPassword))
            {
                TempData["UserLoginFailed"] = "Password is mandatory";
                return RedirectToAction("ChangePassword", "Login");
            }

            if (users.CurrentPassword.Equals(users.NewPassword))
            {
                TempData["UserLoginFailed"] = "Please enter the New Password. Can't update the last Password.";
                return RedirectToAction("ChangePassword", "Login");
            }

            if (!users.ConfirmPassword.Equals(users.NewPassword))
            {
                TempData["UserLoginFailed"] = "New password and confirm password does not match.";
                return RedirectToAction("ChangePassword", "Login");
            }
            users.Password = IJERTSEncryptioncs.Encrypt(CommonHelper.GenerateDynamicPassword(), CommonHelper.SaltPassword, CommonHelper.EncryptKey);
            users.UserId = Convert.ToInt64(HttpContext.Session["UserId"]);
            string sResults = _login.ChangePassword(users);
            return RedirectToAction("Index", "Home");

        }
    }
}