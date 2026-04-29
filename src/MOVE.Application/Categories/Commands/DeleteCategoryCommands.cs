using MediatR;
using MOVE.Application.Interfaces;
using MOVE.Domain.Interfaces;

namespace MOVE.Application.Categories.Commands;

public record DeleteCategoryCommand(int Id) : IRequest<bool>;

public class DeleteCategoryCommandHandler
	: IRequestHandler<DeleteCategoryCommand, bool>
{
	private readonly ICategoryRepository _repository;

	public DeleteCategoryCommandHandler(ICategoryRepository repository)
	{
		_repository = repository;
	}

	public async Task<bool> Handle(
		DeleteCategoryCommand request,
		CancellationToken cancellationToken)
	{
		var category = await _repository.GetByIdAsync(request.Id);

		if (category == null) return false;

		await _repository.DeleteAsync(request.Id);
		return true;
	}
}