using System;

namespace Squawkings.ViewModels
{
	public class SquawkDisplay
	{
		public int UserId { get; set; }
		public string Username { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName { get { return FirstName + " " + LastName; } }
		public string Content { get; set; }
		public DateTime Time { get; set; }
		public string AvatarUrl { get; set; }
		public string Email { get; set; }
		public bool IsGravatar { get; set; }
	}
}