using System.Collections.Generic;
using Squawkings.Models;

namespace Squawkings.ViewModels
{
	public class ProfileViewModel
	{
		public SquawkUser User { get; set; }
		public List<SquawkDisplay> Squawks { get; set; }
		public int Followers { get; set; }
		public int Following { get; set; }
		public bool IsFollowing { get; set; }
	}
}