using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Squawkings.Controllers
{
    public class ProfileController : Controller
    {

        public ActionResult Index(string userName)
        {
        	ViewBag.UserName = userName;
            return View();
        }

    }
}
