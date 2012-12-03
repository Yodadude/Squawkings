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

        public ActionResult Index()
        {
			if (!User.Identity.IsAuthenticated)
				return RedirectToAction("Index", "Global");

        	var vm = new ListSquawkingsViewModel {Squawks = GetSquawkDisplays(), Squawk = new SquawkInputModel()};

        	return View(vm);
        }

		[HttpPost]
		public ActionResult Index(SquawkInputModel model)
		{

			if (!string.IsNullOrEmpty(model.Squawk))
			{
				var newSquawk = new Squawks
				                	{
				                		UserId = User.Identity.Id(),
										Content = model.Squawk.Left(400), 
										CreatedAt = DateTime.Now
				                	};
				IDatabase db = new Database("Squawkings");
				db.Insert(newSquawk);
			}
			else
			{
				ModelState.AddModelError("","The field 'Squawk' is required.");
			}

			return Index();
		}

		[OutputCache(Duration = 10, VaryByParam = "*")]
		public ActionResult Totals()
		{
			var vm = new TotalsViewModel();

			IDatabase db = new Database("Squawkings");

			vm.SquawksTotals = db.ExecuteScalar<int>(@"select count(*) from Squawks");
			vm.UserTotals = db.ExecuteScalar<int>(@"select count(*) from Users");

			return PartialView(vm);
		}


		private List<SquawkDisplay> GetSquawkDisplays()
		{

			IDatabase db = new Database("Squawkings");

			var squawks = db.Fetch<SquawkDisplay>(@"select u.AvatarUrl, u.UserName, u.FirstName + ' '+ u.LastName as FullName, s.CreatedAt as Time, s.Content from Squawks s inner join Users u on u.UserId = s.UserId where u.UserId = @0 or u.UserId in (select UserId from Followers where FollowerUserId = @0) order by s.SquawkId desc", User.Identity.Id());

			return squawks;

		}

    }
}
