using MediatR;
using MOVE.Application.Interfaces;
using MOVE.Domain.Common;

namespace MOVE.Application.Categories.Commands;

public record UpdateCategoryCommand(
	int Id,
	string Name,
	string Description) : IRequest<OperationResult>;

public class UpdateCategoryCommandHandler
	: IRequestHandler<UpdateCategoryCommand, OperationResult>
{
	private readonly ICategoryRepository _repository;

	public UpdateCategoryCommandHandler(ICategoryRepository repository)
	{
		_repository = repository;
	}

	public async Task<OperationResult> Handle(
		UpdateCategoryCommand request,
		CancellationToken cancellationToken)
	{
		var category = await _repository.GetByIdAsync(request.Id);

		if (category == null)
			return OperationResult.FailureResult("Category not found");

		category.Name = request.Name;
		category.Description = request.Description;

		await _repository.UpdateAsync(category);

		return OperationResult.SuccessResult();
	}
}