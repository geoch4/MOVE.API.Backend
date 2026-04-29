using AutoMapper;
using MediatR;
using MOVE.Application.DTOs;
using MOVE.Application.Interfaces;

namespace MOVE.Application.Products.Queries;

public record GetAllProductsQuery() : IRequest<IEnumerable<ProductDto>>;

public class GetAllProductsQueryHandler
	: IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
{
	private readonly IProductRepository _repository;
	private readonly IMapper _mapper;

	public GetAllProductsQueryHandler(IProductRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<ProductDto>> Handle(
		GetAllProductsQuery request,
		CancellationToken cancellationToken)
	{
		var products = await _repository.GetAllAsync();
		return _mapper.Map<IEnumerable<ProductDto>>(products);
	}
}