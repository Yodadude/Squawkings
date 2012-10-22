using System.Web.Mvc;
using System.Web.Security;
using Squawkings.Models;

namespace Squawkings.Controllers
{
    public class LogonController : Controller
    {
        //
        // GET: /Logon/

        public ActionResult Index()
        {
            return View(new LogonInputModel() {Username = "test"});
        }

		[HttpPost]
		public ActionResult Index(LogonInputModel input)
		{
			if (!ModelState.IsValid)
			{
				return Index();
			}

			if (input.Username == "test" && input.Password == "test")
			{
				FormsAuthentication.SetAuthCookie(input.Username, false);
				return RedirectToAction("Index", "Home");
			}

			ModelState.AddModelError("", "Failed to login, you have.");

			return View(input);
		}
    }
}
