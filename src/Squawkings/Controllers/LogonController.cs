using System.Web.Mvc;
using System.Web.Security;
using NPoco;
using Squawkings.Models;
using System.Web.Helpers;

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
		[ValidateAntiForgeryToken]
		public ActionResult Index(LogonInputModel input)
		{

			if (!ModelState.IsValid)
			{
				return Index();
			}

			IDatabase db = new Database("Squawkings", Database.MsSqlClientProvider);

			var logonUser = db.SingleOrDefault<LogonUser>(@"select u.UserId, s.password from Users u inner join UserSecurityInfo s on s.UserId = u.UserId where u.UserName = @0", input.Username);

			if (logonUser != null)
			{
				if (Crypto.VerifyHashedPassword(logonUser.Password, input.Password))
				{
					FormsAuthentication.SetAuthCookie(input.Username, input.RememberMe);
					return RedirectToAction("Index", "Home");
				}
			}

			ModelState.AddModelError("", "Failed to login, you have.");

			return Index();
		}

		private class LogonUser
		{
			public int UserId { get; set; }
			public string Password { get; set; }
		}
    }
}
