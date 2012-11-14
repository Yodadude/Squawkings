using System;
using System.Collections.Generic;
using System.Web.Mvc;
using NPoco;
using Squawkings.Models;

namespace Squawkings.Controllers
{

    public class HomeController : Controller
    {

        public ActionResult Index()
        {
			if (!User.Identity.IsAuthenticated)
				return RedirectToAction("Index", "Global");

			return View(GetSquawkDisplays());
        }

		[HttpPost]
		public ActionResult Index(string squawk)
		{
			if (!string.IsNullOrEmpty(squawk))
			{
				var newSquawk = new Squawks
				                	{
				                		UserId = User.Identity.Id(), 
										Content = squawk.Left(400), 
										CreatedAt = DateTime.Now
				                	};
				IDatabase db = new Database("Squawkings");
				db.Insert(newSquawk);
			}

			return Index();
		}
	
		private List<SquawkDisplay> GetSquawkDisplays()
		{

			IDatabase db = new Database("Squawkings");

			var squawks = db.Fetch<SquawkDisplay>(@"select u.AvatarUrl, u.UserName, u.FirstName + ' '+ u.LastName as FullName, s.CreatedAt as Time, s.Content from Squawks s inner join Users u on u.UserId = s.UserId where u.UserId = @0 or u.UserId in (select UserId from Followers where FollowerUserId = @0) order by s.SquawkId desc", User.Identity.Id());

			//var squawks = new List<SquawkDisplay>
			//                  {
			//                      new SquawkDisplay() { AvatarUrl = "~/Content/images/placeholder-profile-img.jpg", Username = "test", FullName = "Test User", Time = DateTime.Now.AddMinutes(0), Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."},
			//                      new SquawkDisplay() { AvatarUrl = "~/Content/images/placeholder-profile-img2.jpg", Username = "test2", FullName = "Test Two", Time = DateTime.Now.AddMinutes(-1), Content = "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."},
			//                      new SquawkDisplay() { AvatarUrl = "~/Content/images/placeholder-profile-img.jpg", Username = "test", FullName = "Test User", Time = DateTime.Now.AddMinutes(-2), Content = "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."},
			//                      new SquawkDisplay() { AvatarUrl = "~/Content/images/placeholder-profile-img2.jpg", Username = "test2", FullName = "Test Two", Time = DateTime.Now.AddMinutes(-3), Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."},
			//                      new SquawkDisplay() { AvatarUrl = "~/Content/images/placeholder-profile-img2.jpg", Username = "test2", FullName = "Test Two", Time = DateTime.Now.AddMinutes(-4), Content = "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."},
			//                      new SquawkDisplay() { AvatarUrl = "~/Content/images/placeholder-profile-img.jpg", Username = "test", FullName = "Test User", Time = DateTime.Now.AddMinutes(-5), Content = "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."}
			//                  };
			return squawks;

		}

    }
}
