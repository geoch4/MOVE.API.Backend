using MediatR;
using MOVE.Application.Interfaces;
using MOVE.Domain.Common;

namespace MOVE.Application.Categories.Commands;

public record DeleteCategoryCommand(int Id) : IRequest<OperationResult>;

public class DeleteCategoryCommandHandler
	: IRequestHandler<DeleteCategoryCommand, OperationResult>
{
	private readonly ICategoryRepository _repository;

	public DeleteCategoryCommandHandler(ICategoryRepository repository)
	{
		_repository = repository;
	}

	public async Task<OperationResult> Handle(
		DeleteCategoryCommand request,
		CancellationToken cancellationToken)
	{
		var category = await _repository.GetByIdAsync(request.Id);

		if (category == null)
			return OperationResult.FailureResult("Category not found");

		await _repository.DeleteAsync(request.Id);

		return OperationResult.SuccessResult();
	}
}