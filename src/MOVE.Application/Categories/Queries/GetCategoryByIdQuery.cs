using MediatR;
using MOVE.Domain.Entities;
using MOVE.Domain.Interfaces;

namespace MOVE.Application.Categories.Queries;

public record GetCategoryByIdQuery(int Id) : IRequest<Category?>;

public class GetCategoryByIdQueryHandler
	: IRequestHandler<GetCategoryByIdQuery, Category?>
{
	private readonly ICategoryRepository _repository;

	public GetCategoryByIdQueryHandler(ICategoryRepository repository)
	{
		_repository = repository;
	}

	public async Task<Category?> Handle(
		GetCategoryByIdQuery request,
		CancellationToken cancellationToken)
	{
		return await _repository.GetByIdAsync(request.Id);
	}
}