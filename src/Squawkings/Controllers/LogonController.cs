using System.Web.Mvc;
using System.Web.Security;
using NPoco;
using Squawkings.Models;
using System.Web.Helpers;
using Squawkings.ViewModels;

namespace Squawkings.Controllers
{
    public class LogonController : Controller
    {
		private readonly IDatabase _db;
    	private readonly IAuthentication _auth;

    	public LogonController(IDatabase db, IAuthentication auth)
		{
			_db = db;
			_auth = auth;
		}

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

			var logonUser = _db.SingleOrDefault<LogonUser>(@"select u.UserId, s.password from Users u inner join UserSecurityInfo s on s.UserId = u.UserId where u.UserName = @0", input.Username);

			if (logonUser != null)
			{
				if (Crypto.VerifyHashedPassword(logonUser.Password, input.Password))
				{
					//FormsAuthentication.SetAuthCookie(logonUser.UserId.ToString(), input.RememberMe);
					_auth.SetAuthCookie(logonUser.UserId.ToString(), input.RememberMe);
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

		public class LogonUser
		{
			public int UserId { get; set; }
			public string Password { get; set; }
		}
    }

	public interface IAuthentication
	{
		void SetAuthCookie(string userId, bool rememberMe);
	}

	public class SquawkAuthentication : IAuthentication
	{
		public void SetAuthCookie(string userId, bool rememberMe)
		{
			FormsAuthentication.SetAuthCookie(userId, rememberMe);
		}
	}
}
