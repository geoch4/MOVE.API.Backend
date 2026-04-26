using AutoMapper;
using MediatR;
using MOVE.Application.DTOs;
using MOVE.Domain.Interfaces;

namespace MOVE.Application.Products.Queries;

public record GetProductByIdQuery(int Id) : IRequest<ProductDto?>;

public class GetProductByIdQueryHandler
	: IRequestHandler<GetProductByIdQuery, ProductDto?>
{
	private readonly IProductRepository _repository;
	private readonly IMapper _mapper;

	public GetProductByIdQueryHandler(IProductRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<ProductDto?> Handle(
		GetProductByIdQuery request,
		CancellationToken cancellationToken)
	{
		var product = await _repository.GetByIdAsync(request.Id);
		return product == null ? null : _mapper.Map<ProductDto>(product);
	}
}