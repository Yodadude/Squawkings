using System.Collections.Generic;
using System.Web.Mvc;
using NPoco;
using Squawkings.Models;

namespace Squawkings.Controllers
{
    public class GlobalController : Controller
    {
		private readonly IDatabase _db;

		public GlobalController(IDatabase db)
		{
			_db = db;
		}

		public ActionResult Index()
		{
			return View(GetSquawkDisplays());
		}

		private List<SquawkDisplay> GetSquawkDisplays()
		{
			
			//var squawks = _db.Fetch<SquawkDisplay>(@"select u.AvatarUrl, u.UserName, u.FirstName + ' '+ u.LastName as FullName, s.CreatedAt as Time, s.Content from Squawks s inner join Users u on u.UserId = s.UserId order by s.SquawkId desc");

			var squawks = _db.Fetch<SquawkDisplay>(new SquawkListQuery().Template1);

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

	public class TemplateBuilder : SqlBuilder
	{
		public Template Template1 { get; set; }
	}

	public class SquawkListQuery : TemplateBuilder
	{
		public SquawkListQuery()
		{
			Template1 = AddTemplate(@"select u.AvatarUrl, u.UserName, u.FirstName + ' '+ u.LastName as FullName, s.CreatedAt as Time, s.Content from Squawks s inner join Users u on u.UserId = s.UserId order by s.SquawkId desc");
		}
	}
}
