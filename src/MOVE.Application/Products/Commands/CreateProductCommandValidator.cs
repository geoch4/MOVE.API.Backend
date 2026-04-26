using FluentValidation;

namespace MOVE.Application.Products.Commands;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
	public CreateProductCommandValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("Name is required")
			.MaximumLength(200).WithMessage("Name cannot exceed 200 characters");

		RuleFor(x => x.Price)
			.GreaterThan(0).WithMessage("Price must be greater than 0");

		RuleFor(x => x.StockQuantity)
			.GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative");

		RuleFor(x => x.CategoryId)
			.GreaterThan(0).WithMessage("A valid category is required");

		RuleFor(x => x.Description)
			.NotEmpty().WithMessage("Description is required");
	}
}