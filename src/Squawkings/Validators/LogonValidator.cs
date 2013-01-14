using FluentValidation;
using Squawkings.Models;
using Squawkings.ViewModels;

namespace Squawkings.Validators
{
	public class LogonValidator : AbstractValidator<LogonInputModel>
	{
		public LogonValidator()
		{
			RuleFor(x => x.Username)
				.NotEmpty()
				.WithMessage("Username is required.");
			RuleFor(x => x.Password)
				.NotEmpty()
				.WithMessage("Password is required.");
		}
	}
}