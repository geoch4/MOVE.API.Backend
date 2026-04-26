using MediatR;
using MOVE.Domain.Entities;
using MOVE.Domain.Interfaces;

namespace MOVE.Application.Products.Queries;

public record GetAllProductsQuery() : IRequest<IEnumerable<Product>>;

public class GetAllProductsQueryHandler
	: IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
{
	private readonly IProductRepository _repository;

	public GetAllProductsQueryHandler(IProductRepository repository)
	{
		_repository = repository;
	}

	public async Task<IEnumerable<Product>> Handle(
		GetAllProductsQuery request,
		CancellationToken cancellationToken)
	{
		return await _repository.GetAllAsync();
	}
}