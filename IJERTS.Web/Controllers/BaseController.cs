using System;
using System.Web.Mvc;

namespace IJERTS.Web.Controllers
{
    public class BaseController : Controller
    {

        //public ActionResult Index()
        //{
        //    return View();
        //}

        /// <summary>
        /// Gets the UserId.
        /// </summary>
        /// <value>
        /// The UserId.
        /// </value>
        public long UserId
        {
            get
            {
                return (HttpContext.Session["UserId"] == null) ? 0 : Convert.ToInt64(HttpContext.Session["UserId"]);
            }
        }

        /// <summary>
        /// Gets the FirstName
        /// </summary>
        /// <value>
        /// The FirstName
        /// </value>
        public string FirstName
        {
            get
            {
                return (HttpContext.Session["FirstName"].ToString() == null) ? "" : HttpContext.Session["FirstName"].ToString();
            }
        }

        /// <summary>
        /// Gets the LastName
        /// </summary>
        /// <value>
        /// The LastName
        /// </value>
        public string LastName
        {
            get
            {
                return (HttpContext.Session["LastName"].ToString() == null) ? "" : HttpContext.Session["LastName"].ToString();
            }
        }

        /// <summary>
        /// Checks the token.
        /// </summary>
        /// <returns>
        /// Returns login index else null
        /// </returns>
        public ActionResult CheckToken()
        {
            if (HttpContext.Session["UserId"] == null)
            {
                HttpContext.Session.Abandon();
                HttpContext.Session.Clear();                

                return this.RedirectToAction("Authenticate", "Login");
            }
            else
            {
                return null;
            }
        }

        public ActionResult CheckEditorToken()
        {
            if (HttpContext.Session["UserId"] == null)
            {
                HttpContext.Session.Abandon();
                HttpContext.Session.Clear();

                return this.RedirectToAction("Authenticate", "Editor");
            }
            else
            {
                return null;
            }
        }

    }
}