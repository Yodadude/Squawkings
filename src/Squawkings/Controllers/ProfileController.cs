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
		private readonly IDatabase _db;

    	public ProfileController(IDatabase db)
    	{
    		_db = db;
    	}

		[Authorize]
		public ActionResult Index(string userName)
		{
			ViewBag.UserName = userName;

			var vm = new ProfileViewModel();

			vm.User = _db.SingleOrDefault<SquawkUser>(@"select * from Users where UserName = @0", userName);

			if (vm.User == null)
			{
				return View("UserNotFound");
			}

			vm.Squawks =
				_db.Fetch<SquawkDisplay>(
					@"select u.AvatarUrl, u.UserName, u.FirstName + ' ' + u.LastName as FullName, s.CreatedAt as Time, s.Content from Squawks s inner join Users u on u.UserId = s.UserId where u.UserId = @0 order by s.SquawkId desc",
					vm.User.UserId);
			vm.Followers = _db.ExecuteScalar<int>(@"select count(*) from Followers where UserId = @0", vm.User.UserId);
			vm.Following = _db.ExecuteScalar<int>(@"select count(*) from Followers where FollowerUserId = @0", vm.User.UserId);
			vm.IsFollowing =
				_db.ExecuteScalar<int>(@"select count(*) from Followers where UserId = @0 and FollowerUserId = @1", vm.User.UserId,
				                      User.Identity.Id()) == 1;

			return View(vm);

		}

    	[HttpPost]
		[Authorize]
		public ActionResult Index(int userId, string userName)
		{

			var isUserValid = _db.ExecuteScalar<int>(@"select count(*) from Users where UserId = @0 ", userId) == 1;
			var isFollowing = _db.ExecuteScalar<int>(@"select count(*) from Followers where UserId = @0 and FollowerUserId = @1", userId, User.Identity.Id()) == 1;

			using (var tran = _db.GetTransaction())
			{

				if (isUserValid)
				{
					if (isFollowing)
					{
						_db.Delete<Followers>("where UserId = @0 and FollowerUserId = @1", userId, User.Identity.Id());
					}
					else
					{
						_db.Insert(new Followers {FollowerUserId = User.Identity.Id(), UserId = userId});
					}
				}

				tran.Complete();
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

			using (var tran = _db.GetTransaction())
			{

				var user = _db.SingleOrDefault<SquawkUser>(@"select * from Users where UserId = @0 ", User.Identity.Id());

				if (model.IsGravatar)
				{
					user.AvatarUrl = @"http://www.gravatar.com/avatar/" + Crypto.Hash(user.Email, "md5").ToLower() + ".png";
				}
				else
				{
					string fileName =
						model.ImageFile.FileName.Substring(model.ImageFile.FileName.LastIndexOf(@"\", System.StringComparison.Ordinal) + 1);
					model.ImageFile.SaveAs(Server.MapPath("~/Content/dev_images/") + fileName);
					user.AvatarUrl = "~/Content/dev_images/" + fileName;
				}

				user.IsGravatar = model.IsGravatar;

				_db.Update(user);

				tran.Complete();
			}

			return RedirectToAction("Index", new{username = User.Identity.GetName()});
		}
    }

}
