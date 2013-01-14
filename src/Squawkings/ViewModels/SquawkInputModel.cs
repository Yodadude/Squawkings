using System.ComponentModel.DataAnnotations;
using Squawkings.Validators;

namespace Squawkings.ViewModels
{
	[FluentValidation.Attributes.Validator(typeof(SquawkValidator))]
	public class SquawkInputModel
	{
		[DataType(DataType.Text)]
		public string Squawk { get; set; }
	}
}