using System.Collections.Generic;
using Squawkings.ViewModels;

namespace Squawkings.Models
{
	public class ListSquawkingsViewModel
	{
		public SquawkInputModel Squawk { get; set; }
		public List<SquawkDisplay> Squawks { get; set; }
	}
}