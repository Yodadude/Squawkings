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
			//Response.Write(Crypto.HashPassword("password"));
            return View(new LogonInputModel() {Username = "test"});
        }

		[HttpPost]
		public ActionResult Index(LogonInputModel input)
		{

			if (!ModelState.IsValid)
			{
				return Index();
			}

			IDatabase db = new Database("", Database.MsSqlClientProvider);

			var user = db.SingleOrDefault<SquawkUser>("select * from Users where UserName = @0", input.Username);

			if (user == null)
			{
				ModelState.AddModelError("Username", "User does not exist.");
			}
			else
			{
				var dbPassword = db.ExecuteScalar<string>("select password from UserSecurityInfo where UserId = @0", user.UserId);

				if (Crypto.VerifyHashedPassword(dbPassword, input.Password))
				{
					FormsAuthentication.SetAuthCookie(input.Username, false);
					return RedirectToAction("Index", "Home");
				}
			}

			ModelState.AddModelError("", "Failed to login, you have.");

			return Index();
		}
    }
}
