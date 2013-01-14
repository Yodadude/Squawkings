using FluentValidation;
using Squawkings.ViewModels;

namespace Squawkings.Validators
{
	public class SquawkValidator : AbstractValidator<SquawkInputModel>
	{
		public SquawkValidator()
		{
			RuleFor(x => x.Squawk)
				.NotEmpty().WithMessage("Please enter a squawk.");
			RuleFor(x => x.Squawk)
				.Length(0, 400).WithMessage("Squawk length cannot exceed 400 characters.!!!");
		}
	}
}