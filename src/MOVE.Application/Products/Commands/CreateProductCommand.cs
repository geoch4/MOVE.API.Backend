using AutoMapper;
using MediatR;
using MOVE.Application.DTOs;
using MOVE.Domain.Entities;
using MOVE.Application.Interfaces;

namespace MOVE.Application.Products.Commands;

public record CreateProductCommand(
	string Name,
	string Description,
	decimal Price,
	int StockQuantity,
	string ImageUrl,
	int CategoryId) : IRequest<ProductDto>;

public class CreateProductCommandHandler
	: IRequestHandler<CreateProductCommand, ProductDto>
{
	private readonly IProductRepository _repository;
	private readonly IMapper _mapper;

	public CreateProductCommandHandler(IProductRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<ProductDto> Handle(
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
		return _mapper.Map<ProductDto>(product);
	}
}