using MediatR;
using MOVE.Domain.Entities;
using MOVE.Domain.Interfaces;

namespace MOVE.Application.Categories.Commands;

public record CreateCategoryCommand(
	string Name,
	string Description) : IRequest<Category>;

public class CreateCategoryCommandHandler
	: IRequestHandler<CreateCategoryCommand, Category>
{
	private readonly ICategoryRepository _repository;

	public CreateCategoryCommandHandler(ICategoryRepository repository)
	{
		_repository = repository;
	}

	public async Task<Category> Handle(
		CreateCategoryCommand request,
		CancellationToken cancellationToken)
	{
		var category = new Category
		{
			Name = request.Name,
			Description = request.Description
		};

		await _repository.AddAsync(category);
		return category;
	}
}