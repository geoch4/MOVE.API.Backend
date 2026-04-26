using MediatR;
using MOVE.Domain.Entities;
using MOVE.Domain.Interfaces;

namespace MOVE.Application.Products.Queries;

public record GetProductByIdQuery(int Id) : IRequest<Product?>;

public class GetProductByIdQueryHandler
	: IRequestHandler<GetProductByIdQuery, Product?>
{
	private readonly IProductRepository _repository;

	public GetProductByIdQueryHandler(IProductRepository repository)
	{
		_repository = repository;
	}

	public async Task<Product?> Handle(
		GetProductByIdQuery request,
		CancellationToken cancellationToken)
	{
		return await _repository.GetByIdAsync(request.Id);
	}
}