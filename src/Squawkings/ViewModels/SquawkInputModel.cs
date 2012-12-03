using System.ComponentModel.DataAnnotations;

namespace Squawkings.ViewModels
{
	public class SquawkInputModel
	{
		[Required]
		[DataType(DataType.Text)]
		public string Squawk { get; set; }
	}
}