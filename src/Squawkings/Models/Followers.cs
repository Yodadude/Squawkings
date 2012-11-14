using NPoco;

namespace Squawkings.Models
{
	[TableName("Followers")]
	public class Followers
	{
		[Column("UserId")]
		public int? UserId { get; set; }
		[Column("FollowerUserId")]
		public int? FollowerUserId { get; set; }
	}
}