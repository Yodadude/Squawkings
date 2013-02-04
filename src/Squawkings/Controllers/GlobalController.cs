using System.Collections.Generic;
using System.Web.Mvc;
using NPoco;
using Squawkings.ViewModels;

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
			
			var squawks = _db.Fetch<SquawkDisplay>(new SquawkListQuery().Template1);

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
			Template1 = AddTemplate(@"select u.AvatarUrl, u.UserName, u.FirstName, u.LastName, s.CreatedAt as Time, s.Content, u.IsGravatar, u.Email from Squawks s inner join Users u on u.UserId = s.UserId order by s.SquawkId desc");
		}
	}
}
