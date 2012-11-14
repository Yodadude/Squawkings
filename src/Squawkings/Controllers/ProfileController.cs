using System.Web.Mvc;
using NPoco;
using Squawkings.Models;

namespace Squawkings.Controllers
{
    public class ProfileController : Controller
    {

        public ActionResult Index(string userName)
        {
        	ViewBag.UserName = userName;

			IDatabase db = new Database("Squawkings");

			var vm = new ProfileViewModel();

			vm.User = db.SingleOrDefault<SquawkUser>(@"select * from Users where UserName = @0", userName);

			if (vm.User != null)
			{
				vm.Squawks =
					db.Fetch<SquawkDisplay>(
						@"select u.AvatarUrl, u.UserName, u.FirstName + ' ' + u.LastName as FullName, s.CreatedAt as Time, s.Content from Squawks s inner join Users u on u.UserId = s.UserId where u.UserId = @0 order by s.SquawkId desc",
						vm.User.UserId);
				vm.Followers = db.ExecuteScalar<int>(@"select count(*) from Followers where UserId = @0", vm.User.UserId);
				vm.Following = db.ExecuteScalar<int>(@"select count(*) from Followers where FollowerUserId = @0", vm.User.UserId);
				return View(vm);
			}
			else
			{
				ViewBag.UserName = userName;
				return View("UserNotFound");
			}
        
        }

    }

}
