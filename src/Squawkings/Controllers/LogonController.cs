using System.Web.Mvc;
using System.Web.Security;
using NPoco;
using Squawkings.Models;
using System.Web.Helpers;

namespace Squawkings.Controllers
{
    public class LogonController : Controller
    {

        public ActionResult Index()
        {
            return View(new LogonInputModel() {Username = "test"});
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Index(LogonInputModel input)
		{

			if (!ModelState.IsValid)
			{
				return Index();
			}

			IDatabase db = new Database("Squawkings");

			var logonUser = db.SingleOrDefault<LogonUser>(@"select u.UserId, s.password from Users u inner join UserSecurityInfo s on s.UserId = u.UserId where u.UserName = @0", input.Username);

			if (logonUser != null)
			{
				if (Crypto.VerifyHashedPassword(logonUser.Password, input.Password))
				{
					FormsAuthentication.SetAuthCookie(logonUser.UserId.ToString(), input.RememberMe);
					if (!string.IsNullOrEmpty(input.ReturnUrl))
						return Redirect(input.ReturnUrl);
					else
						return RedirectToAction("Index", "Home");
				}
			}

			ModelState.AddModelError("", "Failed to login, you have.");

			return Index();
		}


		public ActionResult Logoff()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Index","Logon");
		}

		private class LogonUser
		{
			public int UserId { get; set; }
			public string Password { get; set; }
		}
    }
}
