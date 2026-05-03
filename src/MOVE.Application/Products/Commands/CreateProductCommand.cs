using AutoMapper;
using MediatR;
using MOVE.Application.DTOs;
using MOVE.Application.Interfaces;
using MOVE.Domain.Common;
using MOVE.Domain.Entities;

namespace MOVE.Application.Products.Commands;

public record CreateProductCommand(
	string Name,
	string Description,
	decimal Price,
	int StockQuantity,
	string ImageUrl,
	int CategoryId) : IRequest<OperationResult<ProductDto>>;

public class CreateProductCommandHandler
	: IRequestHandler<CreateProductCommand, OperationResult<ProductDto>>
{
	private readonly IProductRepository _repository;
	private readonly IMapper _mapper;

	public CreateProductCommandHandler(IProductRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<OperationResult<ProductDto>> Handle(
		CreateProductCommand request,
		CancellationToken cancellationToken)
	{
		var product = new Product
		{
			Name = request.Name,
			Description = request.Description,
			Price = request.Price,
			StockQuantity = request.StockQuantity,
			ImageUrl = request.ImageUrl,
			CategoryId = request.CategoryId,
			CreatedAt = DateTime.UtcNow
		};

		await _repository.AddAsync(product);

		var dto = _mapper.Map<ProductDto>(product);
		return OperationResult<ProductDto>.SuccessResult(dto);
	}
}