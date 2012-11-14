using System;
using NPoco;

namespace Squawkings.Models
{
	[TableName("Squawks")]
	[PrimaryKey("SquawkId")]
	public class Squawks
	{
		[Column("SquawkId")]
		public int SquawkId { get; set; }
		[Column("UserId")]
		public int UserId { get; set; }
		[Column("CreatedAt")]
		public DateTime CreatedAt { get; set; }
		[Column("Content")]
		public string Content { get; set; }
	}
}