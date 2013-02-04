using NPoco;

namespace Squawkings.Models
{
	[TableName("UserSecurityInfo")]
	[PrimaryKey("UserId")]
	public class UserSecurityInfo
	{
		public int UserId { get; set; }
		public string Password { get; set; }
	}
}