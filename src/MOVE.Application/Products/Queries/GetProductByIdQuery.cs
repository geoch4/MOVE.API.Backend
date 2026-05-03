using AutoMapper;
using MediatR;
using MOVE.Application.DTOs;
using MOVE.Application.Interfaces;
using MOVE.Domain.Common;

namespace MOVE.Application.Products.Queries;

public record GetProductByIdQuery(int Id) : IRequest<OperationResult<ProductDto>>;

public class GetProductByIdQueryHandler
	: IRequestHandler<GetProductByIdQuery, OperationResult<ProductDto>>
{
	private readonly IProductRepository _repository;
	private readonly IMapper _mapper;

	public GetProductByIdQueryHandler(IProductRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<OperationResult<ProductDto>> Handle(
		GetProductByIdQuery request,
		CancellationToken cancellationToken)
	{
		var product = await _repository.GetByIdAsync(request.Id);

		if (product == null)
			return OperationResult<ProductDto>.FailureResult("Product not found");

		var dto = _mapper.Map<ProductDto>(product);
		return OperationResult<ProductDto>.SuccessResult(dto);
	}
}