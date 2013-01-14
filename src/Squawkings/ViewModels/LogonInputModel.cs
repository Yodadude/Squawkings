
using System.ComponentModel.DataAnnotations;
using Squawkings.Validators;

namespace Squawkings.ViewModels
{
	[FluentValidation.Attributes.Validator(typeof(LogonValidator))]
	public class LogonInputModel
	{
		public string Username { get; set; }
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public bool	RememberMe { get; set; }
		public string ReturnUrl { get; set; }
	}
}