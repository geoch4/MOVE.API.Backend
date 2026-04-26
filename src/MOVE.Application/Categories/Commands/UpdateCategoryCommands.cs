using MediatR;
using MOVE.Domain.Interfaces;

namespace MOVE.Application.Categories.Commands;

public record UpdateCategoryCommand(
	int Id,
	string Name,
	string Description) : IRequest<bool>;

public class UpdateCategoryCommandHandler
	: IRequestHandler<UpdateCategoryCommand, bool>
{
	private readonly ICategoryRepository _repository;

	public UpdateCategoryCommandHandler(ICategoryRepository repository)
	{
		_repository = repository;
	}

	public async Task<bool> Handle(
		UpdateCategoryCommand request,
		CancellationToken cancellationToken)
	{
		var category = await _repository.GetByIdAsync(request.Id);

		if (category == null) return false;

		category.Name = request.Name;
		category.Description = request.Description;

		await _repository.UpdateAsync(category);
		return true;
	}
}