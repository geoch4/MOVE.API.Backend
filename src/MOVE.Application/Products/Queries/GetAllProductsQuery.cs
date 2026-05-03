using AutoMapper;
using MediatR;
using MOVE.Application.DTOs;
using MOVE.Application.Interfaces;
using MOVE.Domain.Common;

namespace MOVE.Application.Products.Queries;

public record GetAllProductsQuery() : IRequest<OperationResult<IEnumerable<ProductDto>>>;

public class GetAllProductsQueryHandler
	: IRequestHandler<GetAllProductsQuery, OperationResult<IEnumerable<ProductDto>>>
{
	private readonly IProductRepository _repository;
	private readonly IMapper _mapper;

	public GetAllProductsQueryHandler(IProductRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<OperationResult<IEnumerable<ProductDto>>> Handle(
		GetAllProductsQuery request,
		CancellationToken cancellationToken)
	{
		var products = await _repository.GetAllAsync();
		var dto = _mapper.Map<IEnumerable<ProductDto>>(products);
		return OperationResult<IEnumerable<ProductDto>>.SuccessResult(dto);
	}
}