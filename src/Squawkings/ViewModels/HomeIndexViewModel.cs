using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Squawkings.Models;

namespace Squawkings.ViewModels
{
	public class HomeIndexViewModel
	{
		//public SquawkInputModel Squawk { get; set; }
		[DataType(DataType.Text)]
		public string Squawk { get; set; }
		public List<SquawkDisplay> Squawks { get; set; }
	}
}