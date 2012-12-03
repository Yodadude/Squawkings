using System.IO;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using NPoco;
using Squawkings.Models;
using Squawkings.ViewModels;

namespace Squawkings.Controllers
{
    public class ProfileController : Controller
    {

		[Authorize]
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
				vm.IsFollowing = db.ExecuteScalar<int>(@"select count(*) from Followers where UserId = @0 and FollowerUserId = @1", vm.User.UserId, User.Identity.Id()) == 1;
				
				return View(vm);
			}

        	ViewBag.UserName = userName;
        	return View("UserNotFound");
        }

		[HttpPost]
		[Authorize]
		public ActionResult Index(int userId, string userName)
		{
			IDatabase db = new Database("Squawkings");

			var isUserValid = db.ExecuteScalar<int>(@"select count(*) from Users where UserId = @0 ", userId) == 1;
			var isFollowing = db.ExecuteScalar<int>(@"select count(*) from Followers where UserId = @0 and FollowerUserId = @1", userId, User.Identity.Id()) == 1;

			if (isUserValid)
			{
				if (isFollowing)
				{
					db.Delete<Followers>("where UserId = @0 and FollowerUserId = @1", userId, User.Identity.Id());
				}
				else
				{
					db.Insert(new Followers {FollowerUserId = User.Identity.Id(), UserId = userId});
				}
			}

			return RedirectToAction("Index", new {userName});
		}

		[Authorize]
		public ActionResult Upload()
		{
			return View(new ProfileUploadInputModel());
		}

		[HttpPost]
		[Authorize]
		public ActionResult Upload(ProfileUploadInputModel model)
		{
			if (!model.IsGravatar && model.ImageFile == null)
			{
				ModelState.AddModelError("ImageFile", "Please load an image file.");
				return Upload();
			}

			IDatabase db = new Database("Squawkings");
			var user = db.SingleOrDefault<SquawkUser>(@"select * from Users where UserId = @0 ", User.Identity.Id());

			if (model.IsGravatar)
			{
				user.AvatarUrl = @"http://www.gravatar.com/avatar/" + Crypto.Hash(user.Email,"md5").ToLower() + ".png";
			}
			else
			{
				string fileName =
					model.ImageFile.FileName.Substring(model.ImageFile.FileName.LastIndexOf(@"\", System.StringComparison.Ordinal) + 1);
				model.ImageFile.SaveAs(Server.MapPath("~/Content/dev_images/") + fileName);
				user.AvatarUrl = "~/Content/dev_images/" + fileName;
			}

			user.IsGravatar = model.IsGravatar;
			
			db.Update(user);

			return RedirectToAction("Index", new{username = User.Identity.GetName()});
		}
    }

}
