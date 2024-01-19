using FluentValidation;

namespace Api.ViewModels.Validators;

public class CreateProductValidator : AbstractValidator<CreateProductInVm>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(40)
            .WithMessage("Title must be less than 40 character");
    }
}