using System.Collections.Generic;

namespace Squawkings.Models
{
	public class ProfileViewModel
	{
		public SquawkUser User { get; set; }
		public List<SquawkDisplay> Squawks { get; set; }
		public int Followers { get; set; }
		public int Following { get; set; }
	}
}