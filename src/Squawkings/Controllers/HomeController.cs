using System;
using System.Collections.Generic;
using System.Web.Mvc;
using NPoco;
using Squawkings.Models;
using Squawkings.ViewModels;

namespace Squawkings.Controllers
{

    public class HomeController : Controller
    {
    	private readonly IDatabase _db;

    	public HomeController(IDatabase db)
    	{
    		_db = db;
    	}

        public ActionResult Index()
        {
			if (!User.Identity.IsAuthenticated)
				return RedirectToAction("Index", "Global");

        	var vm = new HomeIndexViewModel {Squawks = GetSquawkDisplays(), Squawk = ""};

        	return View(vm);
        }

		[HttpPost]
		public ActionResult Index(SquawkInputModel model)
		{

			if (ModelState.IsValid)
			{
				var newSquawk = new Squawks
				                	{
				                		UserId = User.Identity.Id(),
										Content = model.Squawk.Left(400), 
										CreatedAt = DateTime.Now
				                	};
				_db.Insert(newSquawk);
			}

			return Index();
		}

		[OutputCache(Duration = 10, VaryByParam = "*")]
		public ActionResult Totals()
		{
			var vm = new TotalsViewModel
			         	{
			         		SquawksTotals = _db.ExecuteScalar<int>(@"select count(*) from Squawks"),
			         		UserTotals = _db.ExecuteScalar<int>(@"select count(*) from Users")
			         	};


			return PartialView(vm);
		}


		private List<SquawkDisplay> GetSquawkDisplays()
		{

			//var squawks = _db.Fetch<SquawkDisplay>(@"select u.AvatarUrl, u.UserName, u.FirstName + ' '+ u.LastName as FullName, s.CreatedAt as Time, s.Content from Squawks s inner join Users u on u.UserId = s.UserId where u.UserId = @0 or u.UserId in (select UserId from Followers where FollowerUserId = @0) order by s.SquawkId desc", User.Identity.Id());
			var squawks = _db.SkipTake<SquawkDisplay>(0,20,@"select u.AvatarUrl, u.UserName, u.FirstName + ' '+ u.LastName as FullName, s.CreatedAt as Time, s.Content from Squawks s inner join Users u on u.UserId = s.UserId where u.UserId = @0 or u.UserId in (select UserId from Followers where FollowerUserId = @0) order by s.SquawkId desc", User.Identity.Id());

			return squawks;

		}

    }

}
