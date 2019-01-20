using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IJERTS.Web.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            ActionResult result = this.ValidateEditorToken();
            if (result != null)
            {
                return result;
            }

            return View();
        }
    }
}