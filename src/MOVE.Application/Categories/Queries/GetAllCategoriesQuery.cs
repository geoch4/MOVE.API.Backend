using MediatR;
using MOVE.Domain.Entities;
using MOVE.Domain.Interfaces;

namespace MOVE.Application.Categories.Queries;

public record GetAllCategoriesQuery() : IRequest<IEnumerable<Category>>;

public class GetAllCategoriesQueryHandler
	: IRequestHandler<GetAllCategoriesQuery, IEnumerable<Category>>
{
	private readonly ICategoryRepository _repository;

	public GetAllCategoriesQueryHandler(ICategoryRepository repository)
	{
		_repository = repository;
	}

	public async Task<IEnumerable<Category>> Handle(
		GetAllCategoriesQuery request,
		CancellationToken cancellationToken)
	{
		return await _repository.GetAllAsync();
	}
}