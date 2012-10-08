using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Squawkings.Controllers;

namespace Squawkings.Models
{
	public class ListSquawkingsViewModel
	{
		public string Squawk { get; set; }
		public List<HomeController.SquawkDisplay> Squawks { get; set; }
	}
}