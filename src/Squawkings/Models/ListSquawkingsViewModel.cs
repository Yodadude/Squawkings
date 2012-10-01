using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Squawkings.Models
{
	public class ListSquawkingsViewModel
	{
		public string Squawk { get; set; }
		public List<SquawkDisplay> Squawks { get; set; }
	}
}