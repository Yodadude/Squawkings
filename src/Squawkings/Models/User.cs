using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPoco;

namespace Squawkings.Models
{
	[TableName("Users")]
	[PrimaryKey("UserId", AutoIncrement = true)]
	public class User
	{
		public int UserId { get; set; }
		public string Username { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Bio { get; set; }
		public string AvatarUrl { get; set; }
		public string Email { get; set; }
		public bool IsGravatar { get; set; }

		[Ignore]
		public string FullName { get { return FirstName + LastName; } }
	}
}