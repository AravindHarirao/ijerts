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
            //users.Password = Encryptioncs.RsaEncrypt(users.Password);
            users.UserType = "A";
            Users objUsers = _login.ValidateLogin(users);
            if (!string.IsNullOrEmpty(objUsers.Email))
            {
                if (!users.Password.Equals(objUsers.Password))
                {
                    TempData["UserLoginFailed"] = "Invalid Password. Please try again.";
                    return View("AuthorLogin");
                }
                else
                {
                    HttpContext.Session["UserId"] = objUsers.UserId.ToString();
                    HttpContext.Session["FirstName"] = objUsers.FirstName.ToString();
                    HttpContext.Session["LastName"] = objUsers.LastName.ToString();
                    HttpContext.Session["UserType"] = objUsers.UserType.ToString();
                    HttpContext.Session["Email"] = objUsers.Email.ToString();


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
                return View("AuthorLogin");
            }
        }

        [HttpPost]
        public ActionResult ValidateEditorLogin(Users users)
        {
            //TODO - Need to pass the Session
            users.SessionId = Guid.NewGuid().ToString();
            //users.Password = IJERTSEncryptioncs.RsaEncrypt(users.Password);
            users.UserType = "E";
            Users objUsers = _login.ValidateLogin(users);
            if (!string.IsNullOrEmpty(objUsers.Email))
            {
                if (!users.Password.Equals(objUsers.Password))
                {
                    TempData["UserLoginFailed"] = "Invalid Password. Please try again.";
                    return View("EditorLogin");
                }
                else
                {
                    HttpContext.Session["UserId"] = objUsers.UserId.ToString();
                    HttpContext.Session["FirstName"] = objUsers.FirstName.ToString();
                    HttpContext.Session["LastName"] = objUsers.LastName.ToString();
                    HttpContext.Session["UserType"] = objUsers.UserType.ToString();
                    HttpContext.Session["Email"] = objUsers.Email.ToString();


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
                return View("EditorLogin");
            }
        }

        [HttpPost]
        public ActionResult ValidateReviewerLogin(Users users)
        {
            //TODO - Need to pass the Session
            users.SessionId = Guid.NewGuid().ToString();
            //users.Password = IJERTSEncryptioncs.RsaEncrypt(users.Password);
            users.UserType = "R";
            Users objUsers = _login.ValidateLogin(users);
            if (!string.IsNullOrEmpty(objUsers.Email))
            {
                if (!users.Password.Equals(objUsers.Password))
                {
                    TempData["UserLoginFailed"] = "Invalid Password. Please try again.";
                    return View("ReviewerLogin");
                }
                else
                {
                    HttpContext.Session["UserId"] = objUsers.UserId.ToString();
                    HttpContext.Session["FirstName"] = objUsers.FirstName.ToString();
                    HttpContext.Session["LastName"] = objUsers.LastName.ToString();
                    HttpContext.Session["UserType"] = objUsers.UserType.ToString();
                    HttpContext.Session["Email"] = objUsers.Email.ToString();

                    UserLoginHistory userLoginHistory = new UserLoginHistory();
                    userLoginHistory.UserId = objUsers.UserId;
                    userLoginHistory.SessionId = HttpContext.Session.SessionID;
                    _login.InsertLoginHistory(userLoginHistory);

                    TempData["UserLoginFailed"] = "Logged in Successfully.";
                    return RedirectToAction("Dashboard", "Review");
                }
            }
            else
            {
                TempData["UserLoginFailed"] = "Invalid Username or Password. Please try again.";
                return View("ReviewerLogin");
            }
        }

        [HttpPost]
        public ActionResult ValidateLogin(Users users)
        {
            //TODO - Need to pass the Session
            users.SessionId = Guid.NewGuid().ToString();
            //users.Password = Encryptioncs.RsaEncrypt(users.Password);
            Users objUsers = _login.ValidateLogin(users);
            if (!string.IsNullOrEmpty(objUsers.Email))
            {
                if (users.Password.Equals(objUsers.Password))
                {
                    TempData["UserLoginFailed"] = "Invalid Password. Please try again.";
                    return View("AuthorLogin");
                }
                else
                {
                    HttpContext.Session["UserId"] = objUsers.UserId.ToString();
                    HttpContext.Session["FirstName"] = objUsers.FirstName.ToString();
                    HttpContext.Session["LastName"] = objUsers.LastName.ToString();
                    HttpContext.Session["Email"] = objUsers.Email.ToString();

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
                return View("AuthorLogin");
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
                return View("AuthorLogin");
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
            //TempData["PasswordChangedMessage"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(Users users)
        {

            if (UserId == 0)
            {
                TempData["PasswordChangedMessage"] = "Please log in to use this feature.";
                return RedirectToAction("ChangePassword", "Login");
            }

            if (string.IsNullOrWhiteSpace(users.CurrentPassword) || string.IsNullOrWhiteSpace(users.NewPassword)
                || string.IsNullOrWhiteSpace(users.ConfirmPassword))
            {
                TempData["PasswordChangedMessage"] = "Password is mandatory";
                return RedirectToAction("ChangePassword", "Login");
            }

            if (users.CurrentPassword.Equals(users.NewPassword))
            {
                TempData["PasswordChangedMessage"] = "Please enter the New Password. Can't update the last Password.";
                return RedirectToAction("ChangePassword", "Login");
            }

            if (!users.ConfirmPassword.Equals(users.NewPassword))
            {
                TempData["PasswordChangedMessage"] = "New password and confirm password does not match.";
                return RedirectToAction("ChangePassword", "Login");
            }

            users.Password = users.CurrentPassword;
            users.UserType = HttpContext.Session["UserType"].ToString();
            users.UserId = Convert.ToInt32(HttpContext.Session["UserId"]);
            users.Email = HttpContext.Session["Email"].ToString();

            Users objUsers = _login.ValidateLogin(users);
            if (string.IsNullOrEmpty(objUsers.Email))
            {
                TempData["PasswordChangedMessage"] = "Invalid current password";
                return RedirectToAction("ChangePassword", "Login");

            }
            else
            {
                users.Password = users.NewPassword;
                users.UserId = Convert.ToInt64(HttpContext.Session["UserId"]);
                string sResults = _login.ChangePassword(users);
                if (sResults.Trim().Equals("Success"))
                {
                    Session.Clear();
                    TempData["PasswordChangedMessage"] = "Password changed successfully. Please login again.";
                    return RedirectToAction("ChangePassword", "Login");
                }
                else
                {
                    TempData["PasswordChangedMessage"] = "Password change failed. Please try again.";
                    return RedirectToAction("ChangePassword", "Login");
                }
                //return RedirectToAction("Index", "Home");
            }
        }
    }
}